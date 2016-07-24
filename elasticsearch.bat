@echo off
rem ---------- tools
SET CURL=tools\curl\curl.exe
SET ZIP=tools\7zip\7za.exe

rem ---------- components versions
SET ELASTIC_SEARCH_VERSION=2.3.2

rem ---------- Download packages ----------
if not exist "downloads" mkdir downloads

if not exist "downloads\elasticsearch-%ELASTIC_SEARCH_VERSION%.zip" %CURL% "https://download.elasticsearch.org/elasticsearch/release/org/elasticsearch/distribution/zip/elasticsearch/%ELASTIC_SEARCH_VERSION%/elasticsearch-%ELASTIC_SEARCH_VERSION%.zip" -o downloads\elasticsearch-%ELASTIC_SEARCH_VERSION%.zip
rem --------------------------------------

rem ---------- Unzip packages ----------
rmdir /Q /S temp
mkdir temp

%ZIP% x -otemp downloads\elasticsearch-%ELASTIC_SEARCH_VERSION%.zip

move temp\elasticsearch-%ELASTIC_SEARCH_VERSION% temp\elasticsearch
rem ------------------------------------

rem ---------- Install service ----------
CALL temp\elasticsearch\bin\service.bat install

rem ---------- Start service ----------
CALL temp\elasticsearch\bin\service.bat start

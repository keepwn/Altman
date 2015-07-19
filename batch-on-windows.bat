mkdir Build/ Build/Bin Build/Plugins Build/Services
xcopy Resources/RunNeed/* Build/ /s /e
xcopy Libraries/IronPython/* Build/ /s /e
xcopy Libraries/Sqlite3/* Build/ /s /e
In order to run the application correctly please edit appsettings.Development.json file located Gamestrefa/Classifieds/:
Edit line with connectionString - enter database name, username and a password.

Run the application.

[OPTIONAL]
For providing sending e-mails please edit appsettings.Development.json file located Gamestrefa/Classifieds/:
1. Edit SenderEmail in EmailSettings section - enter e-mail address that will provide sending e-mails from.
2. Edit SenderEmailPassword in EmailSettings section - enter password for email entered above.
3. 3. Edit HostSmtp in EmailSettings section - enter HostSmtp which provides sending e-mails.

To enable data storage in the cloud please edit appsettings.Development.json file located Gamestrefa/Classifieds/:
1. Please create a blob service on https://portal.azure.com/ and create container named 'images'
2. Copy Access key ConnectionString from https://portal.azure.com/ Access Keys section, into "BlobConnection": located Gamestrefa/Classifieds/appsettings.Development.json/ConnectionStrings.

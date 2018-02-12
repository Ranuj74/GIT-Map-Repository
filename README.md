# Map Assignment
 
This is the sample application in which - 
 
The user see a screen with a map and an upload button. The upload button show the user a standard dialog box which only accept CSV files. The user is able to upload a CSV file using this dialog (Test/sample CSV (AddressInput.csv) is attached).

The application parse the uploaded CSV file and split the address into chunks: Unit, Street, Town/City, State, Zip Code, Country.

Display the addresses on a Google/Leaflet map as markers.

Clicking on each address show a table with the split chunks in a popup.

The user is shown a new download button. If the user clicks on the download button, it show them a standard dialog box allowing the user to download a CSV file with the split chunks.

# Steps to Deploy on IIS Server - 

1. Download the code and extract it.
2. Open project in Visual Studio and then right click on your project, and then select “Publish”. It should bring up the dialog.
3. Select “Custom” from the options and enter a profile name for your host and Click “OK” .
4. Now select “File System” as publish method and enter your preferred deployment location.
5. Click “Next” and then select “Release” as the configuration and check the “Delete all existing files prior to publish” option to make sure that Visual Studio will generate fresh files once you re-publish your app.
6. Click “Next” and it should take you to the next step where it will inform you that your web app will be deployed to the location you supplied from the previous step. If you are sure about it then just click “Publish”
7. Visual Studio will compile and publish your app to the desired location. When succeeded then it show Publish Succeeded in the output window.
8. The last step is to configure IIS to convert your app as a web application. To do this open IIS Manager or simply type “inetmgr” in search box.
9. Expand the “Sites” folder and then right click on the “Default Web Site” and select “Add Virtual Directory”.
10. Enter an alias name and then browse the location where you publish the source files for your web app. Now click “OK”. Your folder should be added under “Default Web Site”.
11. Now right click on your folder and select “Convert to Web Application”
12. Click “OK” to convert your folder into a Web Application.

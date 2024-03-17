### Sorting Numbers

## Overview
This is a web application built using ASP.NET Core that allows users to enter a variable amount of numbers, sort them in ascending or descending order, and view the results. The sorted numbers along with the sort direction and time taken to perform the sort are stored in a database.

## Features
Allow users to enter a variable amount of numbers.
Sort the numbers in ascending or descending order based on user selection.
Store the sorted numbers along with the sort direction and time taken to perform the sort in a database.
Display the sorted numbers, sort direction, and time taken to the user.
Provide feedback to the user about the success of the operation, validation issues, or errors.

##Setup
To run the application locally:

Clone the repository to your local machine.
Ensure you have the .NET SDK installed.
Navigate to the root directory of the project in your terminal or command prompt.
Run the command dotnet run to start the application.
Open your web browser and navigate to https://localhost:5001 to access the application.
Technologies Used
ASP.NET Core: Used to build the web application.
Entity Framework Core: Used for database access and management.
HTML/CSS: Used for the front-end user interface.
Razor Pages: Used for server-side rendering and handling of user requests.
C#: Used as the primary programming language.

## Structure
Models: Contains the data models used in the application, such as SortResult and SortedNumber.
Controllers: Contains the controller classes responsible for handling user requests and interactions.
Views: Contains the Razor views that render HTML content to the user.
Choices Made
ASP.NET Core: Chosen for its robustness, performance, and cross-platform capabilities.
Entity Framework Core: Chosen for its ease of use and integration with ASP.NET Core for database operations.
Razor Pages: Chosen for its simplicity and ability to handle server-side logic and rendering in a single file.

## Feedback and Contributions
Feedback and contributions are welcome! Feel free to submit issues or pull requests if you encounter any bugs or have suggestions for improvements.

🖼️ Template Gallery (WPF, MVVM)
This is a sample desktop application built using .NET 8 WPF with the MVVM pattern. It simulates a basic UI for browsing presentation templates with the ability to mark favorites, which are persisted to disk.

🧪 Built for Prezi's Mid-Level .NET Engineer application as a practical demonstration of WPF, MVVM, async programming, and UI state management.

✨ Features
✅ WPF UI built using XAML

✅ MVVM architecture with property binding

✅ Async loading of data and images

✅ UI responsiveness with Task, Dispatcher, and proper threading

✅ Favorite templates with toggle buttons

✅ Persistent favorites using favorites.json

🛠️ Designed for future extension with real APIs

📷 Demo

![image](https://github.com/user-attachments/assets/9ec724b5-fed4-4e88-9d42-dd9019048c54)


🧠 Technologies Used
C# .NET 8

WPF (XAML)

MVVM

ObservableCollection<T>, INotifyPropertyChanged

ICommand with custom RelayCommand

JSON file persistence (System.Text.Json)

Async image loading (BitmapImage, Dispatcher.Invoke)

📂 Architecture
ViewModel: MainVM (handles data and logic)

Model: Template (with properties like Title, Description, ImageUrl, IsFavorite)

Service: TemplateService.cs (stubbed service to simulate fetching template data)

Converters: BoolToStarConverter (toggles star icon in the UI)

🔄 Future Enhancements
🌐 Integration with a real REST API for templates

🗂️ Grouping/filtering by tags or categories

🔍 Search functionality

🧪 Unit tests for the service and ViewModel logic

⚙️ Running the App
Clone the repository
git clone https://github.com/ronanparkinson/TemplateGallery.git

Open in Visual Studio 2022+

Set TemplateGallery as the startup project

Run the app (F5)

🙋 Why this project?
This project demonstrates:

Practical WPF knowledge

Clean MVVM separation

Passion and hands-on effort beyond CV checkboxes

A solid foundation for modern .NET desktop applications

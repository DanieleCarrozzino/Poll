# Poll2 Library
## Overview
The Poll2 library is a versatile tool designed to simplify the creation and management of polls within your applications. It provides an easy-to-use Poll object that encapsulates the entire poll structure, making it straightforward to integrate polls into your user interfaces.

## Features
- __Flexible Design:__ The Poll class extends StackPanel, offering a customizable layout for displaying poll questions, answers, and progress.

- __Rich Visuals:__ Enhance your polls with rich visuals, including icons for poll questions and multiple selection options.

- __User-Friendly Interface:__ The library offers an intuitive interface for users to interact with polls, providing a seamless experience in selecting answers.

- __Dynamic Progress Tracking:__ Automatically tracks and displays the progress of selected answers, making it easy to gauge the popularity of each option.

## Usage
Getting Started
- __Instantiate Poll:__ Create an instance of the Poll class by providing a list of answers and a question string.
```csharp
var pollAnswers = new List<(string, int)> { ("Option A", 1), ("Option B", 2), ("Option C", 3) };
var pollQuestion = "Which option do you prefer?";
var poll = new Poll2.Poll(pollAnswers, pollQuestion);
```
- __Integration:__ Add the Poll instance to your application's UI as needed.
```csharp
yourUiElement.Children.Add(poll);
```
- __Handle Click Events:__ Attach a callback function to handle user click events.
```csharp
poll.ClickAction += (text, id, selected) =>
{
    // Handle the click event, e.g., store user responses.
    Console.WriteLine($"User selected option {id}: {text}, Selected: {selected}");
};
```
- __External Selection:__ Optionally, you can programmatically select answers externally using the selectAnswer method.
```csharp
// Select the answer with id 2 for user with id 123
poll.selectAnswer(2, 123, true);
```
## Example
Here's a simple example demonstrating how to create a poll:

```csharp
var pollAnswers = new List<(string, int)> { ("Yes", 1), ("No", 2), ("Undecided", 3) };
var pollQuestion = "Do you enjoy coding?";
var poll = new Poll2.Poll(pollAnswers, pollQuestion);

// Add the poll to your UI element
yourUiElement.Children.Add(poll);

// Handle click events
poll.ClickAction += (text, id, selected) =>
{
    Console.WriteLine($"User selected option {id}: {text}, Selected: {selected}");
};
```

### Acknowledgments
boh

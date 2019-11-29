# Liquid Technologies Modal Dialogs for Blazor

A simple and customizable Modal Dialog and MessageBox implementation for [Blazor](https://blazor.net)

[![Build Status](https://dev.azure.com/LiquidTechnologies/Blazor/ModalDialog/_apis/build/status/LiquidTechnologies.Blazor.ModalDialog?branchName=master)](https://dev.azure.com/LiquidTechnologies/Blazor/ModalDialog/_build/latest?definitionId=4&branchName=master)

![Nuget](https://img.shields.io/nuget/v/liquidtechnologies.blazor.modaldialog.svg)

![Screenshot of the component in action](screenshot.png)

## Summary
* Simple way to turn a Blazor Component into a Modal Dialog.
* Data can be passed to the Modal Dialog Blazor Component
* Allows values returned from the Modal Dialog Blazor Component to be retrieved.
* Can be used in functions without breaking up the flow of the logic i.e.
```    
    ModalDialogResult result = await ModalDialog.ShowDialogAsync<ConfirmationForm>("Are You Sure");
    if (result.Success)
        DeltetEverything();
```
* Can be nested. i.e. a Modal Dialog can create child Modal Dialogs.
* Include Simple Winforms MessageBox's
 


## Credits

This is a re-work of Chris Sainty's [Blazored.Modal](https://github.com/Blazored/Modal), it builds on the work he has done, but because the approach is quite different and the API is not compatible, I chose to re-package this as a separate project. All namespaces and CSS styles names have been altered to avoid clashes.


## Getting Setup

You can install the package via the nuget package manager just search for *LiquidTechnologies.Blazor.ModalDialog*. You can also install via powershell using the following command.

```powershell
Install-Package LiquidTechnologies.Blazor.ModalDialog
```

Or via the dotnet CLI.

```bash
dotnet add package LiquidTechnologies.Blazor.ModalDialog
```

### 1. Register Services

You will need to add the following using statement and add a call to register the Blazored Modal services in your applications `Startup.ConfigureServices` method.

```csharp
using LiquidTechnologies.Blazor.ModalDialog;

public void ConfigureServices(IServiceCollection services)
{
    services.AddModalDialog();
}
```

### 2. Add Imports

Add the following to your *_Imports.razor*

```csharp
@using LiquidTechnologies.Blazor.ModalDialog
@using LiquidTechnologies.Blazor.ModalDialog.Services
```

### 3. Add Modal Component

Add the `<ModalDialogContainer />` tag into your applications *MainLayout.razor*.

### 4. Add reference to style sheet

Add the following line to the `head` tag of your `_Host.cshtml` (Blazor Server app).

```html
<link href="_content/LiquidTechnologies.Blazor.ModalDialog/liquid.modal-dialog.css" rel="stylesheet" />
```

## Usage

### Displaying the modal

In order to show the modal, you have to inject the `IModalDialogService` into the component or service you want to invoke the modal. 
You can then call the `ShowDialogAsync` method passing in the title for the modal and the type of the component you want the modal to display.

For example, say I have a component called `SignUpForm` which will request the users first and last name.

Once the user has completed the form a ModalDialogResult object is returned with the result.
The data the user submitted in the form can then be read and used (or ignored if the form was cancelled).

```html
@page "/"
@inject IModalDialogService Modal

<button @onclick="SignUpBtn_Clicked" class="btn btn-primary">Sign Up Now</button>

@code {
    async void SignUpBtn_Clicked()
    {
        ModalDialogResult result = await ModalDialog.ShowDialogAsync<SignUpForm>("Sign Up For your free account");
        if (result.Success)
            CreateNewUser(result.ReturnParameters.Get<string>("FirstName"), result.ReturnParameters.Get<string>("LastName"))
    }
}
```
Notice the function `SignUpBtn_Clicked` is `async`. The returned Task<ModalDialogResult> is only completed when the SignUpForm has been closed, allowing for more readable code.

### Passing Parameters

If you need to pass values to the component you are displaying in the modal dialog, then you can use the `ModalDialogParameters` object. 
Any component which is displayed in the modal has access to this object as a `[CascadingParameter]`.

#### Index Component

```html
@page "/"
@inject IModalDialogService Modal

<h1>My Movies</h1>

<ul>
    @foreach (var movie in Movies)
    {
        <li>@movie.Name (@movie.Year) - <button @onclick="@(async () => await ShowEditMovie(movie.Id))" class="btn btn-primary">Edit Movie</button></li>
    }
</ul>

@code {

    List<Movies> Movies { get; set; }

    async void ShowEditMovie(int movieId)
    {
        ModalDialogParameters parameters = new ModalDialogParameters();
        parameters.Add("MovieId", movieId);

        Movie editedMovie = await ModalDialog.ShowDialogAsync<EditMovie>("Edit Movie", new ModalDialogOptions(), parameters);
    }
}
```

#### EditMovie Component

```html
@inject IMovieService MovieService
@inject IModalDialogService Modal

<div class="simple-form">

    <div class="form-group">
        <label for="movie-name">Movie Name</label>
        <input @bind="@Movie.Name" type="text" class="form-control" id="movie-name" />
    </div>

    <div class="form-group">
        <label for="year">Year</label>
        <input @bind="@Movie.Year" type="text" class="form-control" id="year" />
    </div>

    <button @onclick="@SaveMovie" class="btn btn-primary">Submit</button>
    <button @onclick="@Cancel" class="btn btn-secondary">Cancel</button>
</div>

@code {

    [CascadingParameter] ModalDialogParameters Parameters { get; set; }

    int MovieId { get; set; }
    Movie Movie { get; set; }

    protected override void OnInit()
    {
        MovieId = Parameters.Get<int>("MovieId");
        LoadMovie(MovieId);
    }

    void LoadMovie(int movieId)
    {
        Movie = MovieService.Load(movieId);
    }

    void SaveMovie()
    {
        MovieService.Save(Movie);

        ModalDialogParameters returnParameters = new ModalDialogParameters();
        returnParameters.Add("Movie", Movie);
        Modal.Close(true, returnParameters);
    }

    void Cancel()
    {
        Modal.Close(false);
    }
}
```

### Customizing the modal

The modal dialogs can be customized to fit a wide variety of uses. These options can be set using the `ModalDialogOptions` object passed into `ShowDialogAsync`.

#### Hiding the close button

A modal has a close button in the top right hand corner by default. 

```csharp
@code {
    void ShowModal()
    {
        ModalDialogOptions options = new ModalDialogOptions()
        {
            ShowCloseButton = false
        };

        await ModalDialog.ShowDialogAsync<Movies>("My Movies", options);
    }
}
```

#### Disabling background click cancellation

You can disable cancelling the modal by clicking on the background using the `BackgroundClickToClose` parameter.

```csharp
@code {
    void ShowModal()
    {
        ModalDialogOptions options = new ModalDialogOptions()
        {
            BackgroundClickToClose = false
        };

        await ModalDialog.ShowDialogAsync<Movies>("My Movies", options);
    }
}
```

#### Styling the modal

You can set an alternative CSS style for the modal if you want to customize the look and feel. 
This is useful when your web application requires different kinds of modal dialogs, like a warning, confirmation or an input form.

```csharp
@code {
    void ShowModal()
    {
        ModalDialogOptions options = new ModalDialogOptions()
        {
            Style = "acme-modal-dialog-movies"
        };

        await ModalDialog.ShowDialogAsync<Movies>("My Movies", options);
    }
}
```

#### Setting the position

Modal Dialogs are shown in the center of the viewport by default. The modal can be shown in different positions if needed. The positioning is flexible as it is set using CSS styling.

The following positioning styles are available out of the box: `liquid-modal-dialog-center`, `liquid-modal-dialog-topleft`, `liquid-modal-dialog-topright`, `liquid-modal-dialog-bottomleft` and `liquid-modal-dialog-bottomright`. Definitions of these styles are found in `ModalDialogPositionOptions`.


```csharp
@code {
    void ShowModal()
    {
        ModalDialogOptions options = new ModalDialogOptions()
        {
            Position = ModalDialogPositionOptions.TopLeft
        };

        await ModalDialog.ShowDialogAsync<Movies>("My Movies", options);
    }
}
```


### MessageBoxes

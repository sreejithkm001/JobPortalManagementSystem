
$(document).ready(function () {

    $("#divcompanyname").hide();
    $("#divcompanyemail").hide();
    $("#divcompanycontactno").hide();
    $("#divcompanydescription").hide();
    $("#divcompanyphoneno").hide();
    $("#AreYouProvider").change(function () {
        if (this.checked) {
            $("#divcompanyname").show();
            $("#divcompanyemail").show();
            $("#divcompanycontactno").show();
            $("#divcompanydescription").show();
            $("#divcompanyphoneno").show();
        }
        else {
            $("#divcompanyname").hide();
            $("#divcompanyemail").hide();
            $("#divcompanycontactno").hide();
            $("#divcompanydescription").hide();
            $("#divcompanyphoneno").hide();
        }
    });
});

///Check username
function Checkusername() {
    var userNameInput = document.getElementById("fname");
    var userName = userNameInput.value.trim();
    var namePattern = /^[A-Za-z\s]+$/; // Only letters and spaces allowed
    var nameErrorSpan = document.getElementById("nameError");

    if (!namePattern.test(userName)) {
        nameErrorSpan.textContent = "Please enter a valid name with only letters and spaces.";
        userNameInput.value = ""; // Clear the input field
        userNameInput.focus();    // Set focus back to the input field
    } else {
        nameErrorSpan.textContent = "";
    }
}

//mobile number validation
function CheckContactNo() {
    var contactNoInput = document.getElementById("mob");
    var contactNo = contactNoInput.value.trim();
    var contactNoErrorSpan = document.getElementById("contactNoError");

    // Define your strong contact number validation criteria here
    var contactNoPattern = /^\d{10}$/; // 10-digit number

    if (!contactNoPattern.test(contactNo)) {
        contactNoErrorSpan.textContent = "Please enter a valid 10-digit contact number.";
    } else {
        contactNoErrorSpan.textContent = "";
    }
}
// check email
function CheckEmail() {
    var emailInput = document.getElementById("mail");
    var email = emailInput.value.trim();
    var emailErrorSpan = document.getElementById("emailError");

    // Define your strong email validation criteria here
    var emailPattern = /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/;

    if (!emailPattern.test(email)) {
        emailErrorSpan.textContent = "Please enter a valid email address.";
    } else {
        emailErrorSpan.textContent = "";
    }
}
//Password validation
function CheckPassword() {
    var passwordInput = document.getElementById("password");
    var password = passwordInput.value.trim();
    var passwordErrorSpan = document.getElementById("passwordError");

    // Define your strong password validation criteria here
    var hasUpperCase = /[A-Z]/.test(password);
    var hasLowerCase = /[a-z]/.test(password);
    var hasDigit = /\d/.test(password);
    var isLengthValid = password.length >= 8;

    if (!(hasUpperCase && hasLowerCase && hasDigit && isLengthValid)) {
        passwordErrorSpan.textContent = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit.";
    } else {
        passwordErrorSpan.textContent = "";
    }
}

//companyname validation
function validateCompanyName(inputField) {
    var companyName = inputField.value;
    var errorDiv = document.getElementById("companyNameError");

    if (companyName.trim() === "") {
        errorDiv.textContent = "Company name is required.";
        inputField.classList.add("invalid");
    } else {
        errorDiv.textContent = "";
        inputField.classList.remove("invalid");
    }
}

//companymobile validation
function validatePhoneNumber() {
    var phoneNumberInput = document.getElementById("phoneNo");
    var phoneNumber = phoneNumberInput.value;
    var phoneRegex = /^[0-9]{10}$/; // Assuming a 10-digit phone number format

    var errorDiv = document.getElementById("phoneNoError");

    if (!phoneRegex.test(phoneNumber)) {
        errorDiv.textContent = "Please enter a valid 10-digit phone number.";
        phoneNumberInput.classList.add("invalid");
    } else {
        errorDiv.textContent = "";
        phoneNumberInput.classList.remove("invalid");
    }
}

// JavaScript function to validate email address
//function validateEmail() {
//    var emailInput = document.getElementById("mail");
//    var emailError = document.getElementById("emailError");
//    var email = emailInput.value.trim();

//    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2, 4}$/;

//    if (!emailPattern.test(email)) {
//        emailError.textContent = "Invalid email address";
//        emailInput.classList.add("is-invalid");
//        emailInput.classList.remove("is-valid");
//        return false;
//    } else {
//        emailError.textContent = "";
//        emailInput.classList.remove("is-invalid");
//        emailInput.classList.add("is-valid");
//        return true;
//    }
//}

//// Attach the validation function to the input's blur event
//var emailInput = document.getElementById("mail");
//emailInput.addEventListener("blur", validateEmail);



function validateDescription(inputField) {
    var companydescription = inputField.value;
    var errorDiv = document.getElementById("descriptionError");

    if (companydescription.trim() === "") {
        errorDiv.textContent = "Company description is required.";
        inputField.classList.add("invalid");
    } else {
        errorDiv.textContent = "";
        inputField.classList.remove("invalid");
    }
}

function Checkemail() {
    var emailinput = document.getElementById("mail");
    var email = emailinput.value.trim();
    var emailerrorSpan = document.getElementById("emailerror");

    // Define your strong email validation criteria here
    var emailpattern = /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/;

    if (!emailpattern.test(email)) {
        emailerrorSpan.textContent = "Please enter a valid email address.";
    } else {
        emailerrorSpan.textContent = "";
    }
}



//Login validation


//check username
function Checkemail() {
    var emailInput = document.getElementById("name");
    var email = emailInput.value.trim();
    var emailErrorSpan = document.getElementById("emailerror");

    // Define your strong email validation criteria here
    var emailPattern = /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/;

    if (!emailPattern.test(email)) {
        emailErrorSpan.textContent = "Please enter a valid email address.";
    } else {
        emailErrorSpan.textContent = "";
    }
}
//check password
function Checkpassword() {
    var passwordInput = document.getElementById("password");
    var password = passwordInput.value.trim();
    var passwordErrorSpan = document.getElementById("passwordError");

    // Define your strong password validation criteria here
    var hasUpperCase = /[A-Z]/.test(password);
    var hasLowerCase = /[a-z]/.test(password);
    var hasDigit = /\d/.test(password);
    var isLengthValid = password.length >= 8;

    if (!(hasUpperCase && hasLowerCase && hasDigit && isLengthValid)) {
        passwordErrorSpan.textContent = "Please enter a valid password.";
    } else {
        passwordErrorSpan.textContent = "";
    }
}
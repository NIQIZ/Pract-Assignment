function validateFullName() {
    var fullname = document.getElementById("fullname-input").value;
    var regex = /^[a-zA-Z ]+$/;

    return regex.test(fullname);
}

function validateCreditCard() {
    var creditcard = document.getElementById("creditcard-input").value;
    var regex = /^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$/;
    
    return regex.test(creditcard);
}

function validateMobileNumber() {
    var mobilenum = document.getElementById("mobile-input").value;
    var regex = /^[689][0-9]{7}$/;
    
    return regex.test(mobilenum);
}

function validateAddress() {
    var address = document.getElementById("address-input").value;
    var regex = /^[a-zA-Z0-9 #-]+$/;

    return regex.test(address);
}

function validateEmail() {
    var email = document.getElementById("email-input").value;
    var regex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    return regex.test(email);
}

function validatePassword() {
    var password = document.getElementById("password-input").value;
    var regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#\$%\^&\*_])[a-zA-Z\d!@#\$%\^&\*_]{12,}$/;

    return regex.test(password)
}

function validateConfirmPassword(){
    var password = document.getElementById("password-input").value;
    var confirm = document.getElementById("confirm-input").value;
    
    return (password === confirm);
}

document.getElementById("registerForm").addEventListener("submit", function(event) {
    const errorUl = document.getElementById("error-ul");
    errorUl.innerHTML = "";
    var form = document.getElementById("registerForm");
    var canSubmit = true;
    var errors = [];
    
    if (!form.checkValidity()){
        errors.push("Please enter all required fields");
    }
    if (!validateFullName()){
        errors.push("Please enter a valid full name");
    }
    if (!validateCreditCard()){
        errors.push("Please enter a valid credit card");
    }
    
    if (!validateMobileNumber()){
        errors.push("Please enter a valid mobile number")
    }
    
    if (!validateAddress()){
        errors.push("Please enter a valid address");
    }
    
    if (!validateEmail()){
        errors.push("Please enter a valid email address");
    }
    
    if (!validatePassword()){
        errors.push("Please enter a valid password");
    }
    
    if (!validateConfirmPassword()){
        errors.push("Please ensure that the password matches");
    }
    
    if (errors.length > 0){
        canSubmit = false;
    }
    
    if (!canSubmit){
        event.preventDefault();
        document.getElementById("error-col").hidden = false;
        for (var i = 0; i < errors.length; i++){
            let node = document.createElement("li");
            let textnode = document.createTextNode(errors[i]);
            node.appendChild(textnode);
            node.classList.add("text-danger");
            errorUl.appendChild(node);
        }
    }
    
    
})

const myPassMeter = passwordStrengthMeter({
    containerElement: '#pswmeter',
    passwordInput: '#password-input',
    showMessage: true,
    messageContainer: '#pswmeter-message',
    pswMinLength: 12,
    borderRadius: 3
});

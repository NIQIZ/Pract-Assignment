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

    if (!validateFileExtension()){
        errors.push("Please input a file with .jpg extension only");
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

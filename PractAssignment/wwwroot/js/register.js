function validateFullName(fullName) {
    // Regular expression to match only alphabets and spaces
    let regex = /^[a-zA-Z\s]+$/;

    // Check if fullName is empty or doesn't match the regular expression
    if (fullName === '' || !regex.test(fullName)) {
        return false;
    }
    return true;
}

function validateMastercard(cardNumber) {
    // Regular expression to match a Mastercard number format
    let regex = /^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$/;

    // Check if cardNumber is empty or doesn't match the regular expression
    if (cardNumber === '' || !regex.test(cardNumber)) {
        return false;
    }
    return true;
}

function validateSingaporeMobile(number) {
    // Regular expression to match a Singapore mobile number format
    let regex = /^(\+65|65|0){0,2}[8,9]{1}[0-9]{7}$/;

    // Check if number is empty or doesn't match the regular expression
    if (number === '' || !regex.test(number)) {
        return false;
    }
    return true;
}

function validateAddress(address) {

    // Regular expression to match only alphabets, numbers, and spaces
    let regex = /^[a-zA-Z0-9\s]+$/;

    // Check if text is empty or doesn't match the regular expression
    if (address === '' || !regex.test(address)) {
        return false;
    }
    return true;
}

function validateEmail(email) {
    // Regular expression to match a valid email address format
    let regex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    // Check if email is empty or doesn't match the regular expression
    if (email === '' || !regex.test(email)) {
        return false;
    }
    return true;
}

let forms = document.querySelectorAll(".needs-validation");

Array.prototype.slice.call( forms ).forEach( function (form) {
    form.addEventListener("submit", function (event){
        if (!form.checkValidity()) {
            event.preventDefault();
            event.stopPropagation();    
        }
        
        form.classList.add("was-validated");
    },false);
});

const myPassMeter = passwordStrengthMeter({
    containerElement: '#pswmeter',
    passwordInput: '#password-input',
    showMessage: true,
    messageContainer: '#pswmeter-message',
    pswMinLength: 12,
    borderRadius: 3
});

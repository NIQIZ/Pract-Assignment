document.getElementById("forget-password-form").addEventListener("submit", function(event) {
    const errorUl = document.getElementById("error-ul");
    errorUl.innerHTML = "";
    var form = document.getElementById("forget-password-form");
    var canSubmit = true;
    var errors = [];

    if (!form.checkValidity()){
        errors.push("Please enter all required fields");
    }

    if (!validateEmail()){
        errors.push("Please enter a valid email")
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


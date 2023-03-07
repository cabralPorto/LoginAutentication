

function mostrarSenha() {
    const passwordInput = document.getElementById("txtSenha");
    const eyeIcon = document.querySelector(".locked");
    const iconTitle = document.querySelector("#iconeSenha");


    if (passwordInput.type === "password") {
        passwordInput.type = "text";
        eyeIcon.classList.replace("glyphicon-eye-open", "glyphicon-eye-close");
        iconTitle.setAttribute("title", "Ocultar senha");
    } else {
        passwordInput.type = "password";
        eyeIcon.classList.replace("glyphicon-eye-close", "glyphicon-eye-open");
        iconTitle.setAttribute("title", "Mostrar senha");
    }
}

















const form = document.querySelector('.js-form');
const inputBoxes = form.querySelectorAll('.js-input');
const inputs = document.querySelectorAll('.js-input-field');
const visibilityButtons = document.querySelectorAll('.js-visibility-icon');
const submitButton = document.querySelector('.js-submit');
const authInputs = document.querySelectorAll('.js-input-authentication');
const authButton = form.querySelector('.js-authentication-button');

// isFilled start. This script allows the label to remain in the upper position (class isFilled) when the input is filled + removes isError class if the input is filled respectively
inputBoxes.forEach((item) => {
    const input = item.querySelector('.js-input-field');
    const label = item.querySelector('.js-input-label');

    handleInput(item, input, label);
    item.addEventListener("input", () => {
        handleInput(item, input, label)
    });
});

function handleInput(item, input, label) {
    if (input.value) {
        label.classList.add('isFilled');
        item.classList.remove('isError');
    } else {
        label.classList.remove('isFilled');
    }
}
// isFilled end.

// show password start. This script toggles password visibility when clicking on 'an eye' button
function handleClick(item) {
    const input = item.parentElement.querySelector('input');

    if (input.getAttribute('type') === 'password') {
        input.setAttribute('type', 'text');
    } else {
        input.setAttribute('type', 'password');
    }
}
// show password end

// isError start. This is an auxillary demonstrative script that mimics validation of the fields adding an error text to each field that is emply when clicking on the submit button.
visibilityButtons.forEach((item) => {
    item.addEventListener("click", () => {
        handleClick(item);
    });
});

if (submitButton) {
    submitButton.addEventListener('click', (e) => {
        e.preventDefault();

        inputBoxes.forEach((item) => {
            const label = item.querySelector('.js-input-label');

            if (!label.classList.contains('isFilled')) {
                item.classList.add('isError');
            } else {
                item.classList.remove('isError');
            }
        })
    });
}
// isError end.

// is-disabled start. Enables button (and vise versa) with is-disabled class in profile authentication if inputs are filled.
if (authInputs && authButton) {
    form.addEventListener("input", () => {
        let itemsFilled = 0;

        authInputs.forEach((item) => {
            if (item.value)
                itemsFilled++;
        });
        (itemsFilled === authInputs.length) ? authButton.classList.remove('is-disabled') : authButton.classList.add('is-disabled');
    });
}
// is-disabled end.

// submitButton start. Uses URL on submit button when fields are not empty.
if (submitButton) {
    form.addEventListener("input", () => {
        let itemsFilled = 0;

        inputs.forEach((item) => {
            if (item.value.length) {
                itemsFilled++;
            }
        });

        if (itemsFilled === inputs.length) {
            submitButton.addEventListener("click", () => {handleSubmitClick()});
        }
    });

    function handleSubmitClick() {
        window.location = submitButton.href;
    }
}

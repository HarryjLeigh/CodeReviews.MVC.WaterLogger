document.addEventListener('DOMContentLoaded', () => {
    const inputs = document.querySelectorAll(".resize");
    const boxes = document.querySelectorAll(".input__box");

    inputs.forEach((input, index) => {
        const box = boxes[index];

        if (input.value.trim() !== "") box.classList.remove("increaseWidthInput");

        input.addEventListener("input", () => {
            if (input.value.trim() === "") {
                box.classList.add("increaseWidthInput");
            } else {
                box.classList.remove("increaseWidthInput");
            }
        });
    });
});

document.addEventListener('DOMContentLoaded', () => {
    calculateWaterAmount();
})


function calculateWaterAmount() {
    const data = document.getElementById("records")

    let totalLitres = 0;
    let size = "";
    for (let i = 1; i < data.rows.length; i++) {

        let amount = parseFloat(data.rows[i].cells[1].innerHTML);
        // console.log(amount);
        size = data.rows[i].cells[2].innerHTML;

        totalLitres += calculateResult(amount, size);
    }
    inputResultToHtml(totalLitres);
}

function calculateResult(waterAmount, size) {
    let glassSize = 0

    if (size.toLowerCase().trim() === "big bottle") {
        glassSize = 1000;
    } else if (size.toLowerCase().trim() === "bottle") {
        glassSize = 500;
    } else if (size.toLowerCase().trim() === "glass") {
        glassSize = 250;
    }
    return glassSize * waterAmount;
}

function inputResultToHtml(litres) {
    const resultArea = document.getElementById("result")

    if (litres > 0) {
        if (litres >= 1000) {
            let litresInDecimal = (litres / 1000).toFixed(2);
            resultArea.innerHTML = `${litresInDecimal} Litres`;
        } else if (litres < 1000) {
            resultArea.innerHTML = `${litres} ml`;
        }
    }
}
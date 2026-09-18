document.addEventListener("DOMContentLoaded", async () => {
    try {
        const response = await fetch('./data/cultures.json');
        if (!response.ok) {
            throw new Error("Failed to load cultures.");
        }

        const cultures = await response.json();
        const cultureKeys = Object.keys(cultures)
        const delimitedString = cultureKeys.join(', ');
        const inputElement = document.getElementById("cultureList");
        if (inputElement) {
            inputElement.placeholder = `e.g., ${delimitedString}`;
        }
    } catch (error) {
        console.error(error.message);
    }

    document.getElementById("nameForm").addEventListener("submit", async function (event) {
        event.preventDefault();

        const cultures = document.getElementById("cultureList").value;
        const numberOfNames = document.getElementById("numberOfNames").value;

        try {
            const response = await fetch(`/generate-names?cultures=${cultures}&numberOfNames=${numberOfNames}`);
            if (!response.ok) {
                throw new Error("Failed to fetch names. Please check your input.");
            }

            const data = await response.json();
            displayResults(data);
        } catch (error) {
            alert(error.message);
        }
    });

    function populateCultureList(cultureKeys) {
        const cultureList = document.getElementById("cultureList");
        cultureKeys.forEach(culture => {
            const option = document.createElement("option");
            option.value = culture;
            cultureList.appendChild(option);
        });
    }

    function displayResults(data) {
        const cultureGrid = document.getElementById("cultureGrid");
        cultureGrid.innerHTML = ""; // Clear previous results

        data.forEach(group => {
            // Create a column for each culture group
            const column = document.createElement("div");
            column.classList.add("grid-item");

            // Add culture name as a header
            const cultureHeader = document.createElement("h3");
            cultureHeader.textContent = `Culture: ${group.culture}`;
            column.appendChild(cultureHeader);

            // Add names as a list
            const nameList = document.createElement("ul");
            group.names.forEach(name => {
                const listItem = document.createElement("li");
                listItem.textContent = `${name.firstName} ${name.lastName}`;
                nameList.appendChild(listItem);
            });
            column.appendChild(nameList);

            // Add the column to the grid
            cultureGrid.appendChild(column);
        });
    }
});
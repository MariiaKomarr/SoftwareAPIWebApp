const typeUri = '/api/softwaretypes';
let softwareTypes = [];

function getSoftwareTypes() {
    fetch(typeUri)
        .then(response => response.json())
        .then(data => displaySoftwareTypes(data))
        .catch(error => console.error('Unable to get software types.', error));
}

function addSoftwareType() {
    const nameInput = document.getElementById('add-name');
    const descInput = document.getElementById('add-description');

    const softwareType = {
        name: nameInput.value.trim(),
        description: descInput.value.trim()
    };

    fetch(typeUri, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(softwareType)
    })
        .then(response => {
            if (response.ok) {
                getSoftwareTypes();
                nameInput.value = '';
                descInput.value = '';
                alert('Software type added successfully.');
            } else {
                alert('Failed to add software type.');
            }
        })
        .catch(error => {
            console.error('Error while adding software type:', error);
            alert('An error occurred while adding the software type.');
        });
}


function deleteSoftwareType(id) {
    fetch(`${typeUri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getSoftwareTypes())
        .catch(error => console.error('Unable to delete software type.', error));
}

function displayEditForm(id) {
    const type = softwareTypes.find(t => t.typeId === id);
    document.getElementById('edit-id').value = type.typeId;
    document.getElementById('edit-name').value = type.name;
    document.getElementById('edit-description').value = type.description;
    document.getElementById('editForm').style.display = 'block';
}

function updateSoftwareType() {
    const id = document.getElementById('edit-id').value;
    const softwareType = {
        typeId: parseInt(id),
        name: document.getElementById('edit-name').value.trim(),
        description: document.getElementById('edit-description').value.trim()
    };

    fetch(`${typeUri}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(softwareType)
    })
        .then(() => getSoftwareTypes())
        .catch(error => console.error('Unable to update software type.', error));

    closeInput();
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function displaySoftwareTypes(data) {
    const tBody = document.getElementById('softwaretypes');
    tBody.innerHTML = '';
    softwareTypes = data;

    data.forEach(t => {
        let row = tBody.insertRow();
        row.insertCell(0).innerText = t.typeId;
        row.insertCell(1).innerText = t.name;
        row.insertCell(2).innerText = t.description;

        let editBtn = document.createElement('button');
        editBtn.textContent = 'Edit';
        editBtn.onclick = () => displayEditForm(t.typeId);

        let deleteBtn = document.createElement('button');
        deleteBtn.textContent = 'Delete';
        deleteBtn.onclick = () => deleteSoftwareType(t.typeId);

        row.insertCell(3).appendChild(editBtn);
        row.insertCell(4).appendChild(deleteBtn);
    });
}

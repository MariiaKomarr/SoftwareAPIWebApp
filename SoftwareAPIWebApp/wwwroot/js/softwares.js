const softwareUri = '/api/softwares';

function getSoftwares() {
    fetch(softwareUri)
        .then(response => response.json())
        .then(data => displaySoftwares(data))
        .catch(error => console.error('Unable to get softwares.', error));
}

function addSoftware() {
    const software = {
        name: document.getElementById('add-name').value.trim(),
        version: document.getElementById('add-version').value.trim(),
        typeId: parseInt(document.getElementById('add-typeId').value),
        author: document.getElementById('add-author').value.trim(),
        usageTerms: document.getElementById('add-usageTerms').value.trim(),
        dateAdded: document.getElementById('add-dateAdded').value,
        annotation: document.getElementById('add-annotation').value.trim()
    };

    fetch(softwareUri, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(software)
    })
        .then(() => getSoftwares())
        .catch(error => console.error('Unable to add software.', error));
}

function deleteSoftware(id) {
    fetch(`${softwareUri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getSoftwares())
        .catch(error => console.error('Unable to delete software.', error));
}

function displayEditForm(id) {
    const software = softwares.find(s => s.softwareId === id);
    document.getElementById('edit-id').value = software.softwareId;
    document.getElementById('edit-name').value = software.name;
    document.getElementById('edit-version').value = software.version;
    document.getElementById('edit-typeId').value = software.typeId;
    document.getElementById('edit-author').value = software.author;
    document.getElementById('edit-usageTerms').value = software.usageTerms;
    document.getElementById('edit-dateAdded').value = software.dateAdded;
    document.getElementById('edit-annotation').value = software.annotation;
    document.getElementById('editForm').style.display = 'block';
}

function updateSoftware() {
    const id = document.getElementById('edit-id').value;
    const software = {
        softwareId: parseInt(id),
        name: document.getElementById('edit-name').value.trim(),
        version: document.getElementById('edit-version').value.trim(),
        typeId: parseInt(document.getElementById('edit-typeId').value),
        author: document.getElementById('edit-author').value.trim(),
        usageTerms: document.getElementById('edit-usageTerms').value.trim(),
        dateAdded: document.getElementById('edit-dateAdded').value,
        annotation: document.getElementById('edit-annotation').value.trim()
    };

    fetch(`${softwareUri}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(software)
    })
        .then(() => getSoftwares())
        .catch(error => console.error('Unable to update software.', error));

    closeInput();
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function displaySoftwares(data) {
    const tBody = document.getElementById('softwares');
    tBody.innerHTML = '';
    softwares = data;

    data.forEach(s => {
        let row = tBody.insertRow();
        row.insertCell(0).innerText = s.name;
        row.insertCell(1).innerText = s.version;
        row.insertCell(2).innerText = s.typeId;
        row.insertCell(3).innerText = s.author;
        row.insertCell(4).innerText = new Date(s.dateAdded).toLocaleString();

        let editBtn = document.createElement('button');
        editBtn.textContent = 'Edit';
        editBtn.onclick = () => displayEditForm(s.softwareId);

        let deleteBtn = document.createElement('button');
        deleteBtn.textContent = 'Delete';
        deleteBtn.onclick = () => deleteSoftware(s.softwareId);

        row.insertCell(5).appendChild(editBtn);
        row.insertCell(6).appendChild(deleteBtn);
    });
}

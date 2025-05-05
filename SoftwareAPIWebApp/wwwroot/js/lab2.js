const uri = 'api/Softwares';
let softwares = [];

function getSoftwares() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displaySoftwares(data))
        .catch(error => console.error('Unable to get softwares.', error));
}

function addSoftware() {
    const name = document.getElementById('add-name').value.trim();
    const version = document.getElementById('add-version').value.trim();
    const typeId = parseInt(document.getElementById('add-typeId').value, 10);
    const author = document.getElementById('add-author').value.trim();
    const usageTerms = document.getElementById('add-usageTerms').value.trim();
    const dateAdded = document.getElementById('add-dateAdded').value;
    const annotation = document.getElementById('add-annotation').value.trim();

    const software = { name, version, typeId, author, usageTerms, dateAdded, annotation };

    fetch(uri, {
        method: 'POST',
        headers: { 'Accept': 'application/json', 'Content-Type': 'application/json' },
        body: JSON.stringify(software)
    })
        .then(response => response.json())
        .then(() => {
            getSoftwares();
            document.querySelectorAll('#addForm input').forEach(i => i.value = '');
        })
        .catch(error => console.error('Unable to add software.', error));
}

function deleteSoftware(id) {
    fetch(`${uri}/${id}`, { method: 'DELETE' })
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
    const id = parseInt(document.getElementById('edit-id').value, 10);
    const software = {
        softwareId: id,
        name: document.getElementById('edit-name').value.trim(),
        version: document.getElementById('edit-version').value.trim(),
        typeId: parseInt(document.getElementById('edit-typeId').value, 10),
        author: document.getElementById('edit-author').value.trim(),
        usageTerms: document.getElementById('edit-usageTerms').value.trim(),
        dateAdded: document.getElementById('edit-dateAdded').value,
        annotation: document.getElementById('edit-annotation').value.trim()
    };

    fetch(`${uri}/${id}`, {
        method: 'PUT',
        headers: { 'Accept': 'application/json', 'Content-Type': 'application/json' },
        body: JSON.stringify(software)
    })
        .then(() => getSoftwares())
        .catch(error => console.error('Unable to update software.', error));

    closeInput();
    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displaySoftwares(data) {
    const tBody = document.getElementById('softwares');
    tBody.innerHTML = '';

    const button = document.createElement('button');

    data.forEach(software => {
        let tr = tBody.insertRow();

        tr.insertCell(0).appendChild(document.createTextNode(software.name));
        tr.insertCell(1).appendChild(document.createTextNode(software.version));
        tr.insertCell(2).appendChild(document.createTextNode(software.typeId));
        tr.insertCell(3).appendChild(document.createTextNode(software.author));
        tr.insertCell(4).appendChild(document.createTextNode(software.dateAdded));

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${software.softwareId})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteSoftware(${software.softwareId})`);

        tr.insertCell(5).appendChild(editButton);
        tr.insertCell(6).appendChild(deleteButton);
    });

    softwares = data;
}

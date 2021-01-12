const uri = 'api/players';
let todos = [];


// Get the modal
var modal = document.getElementById("myModal");

// Get the button that opens the modal
var btn = document.getElementById("myBtn");

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks the button, open the modal 
btn.onclick = function() {
  modal.style.display = "block";
}

// When the user clicks on <span> (x), close the modal
span.onclick = function() {
  modal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function(event) {
  if (event.target == modal) {
    modal.style.display = "none";
  }
}

function getItems() {
  fetch(uri)
    .then(response => response.json())
    .then(data => _displayItems(data))
    .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
  const addNameTextbox1 = document.getElementById('add-first_name');
  const addNameTextbox2 = document.getElementById('add-last_name');
  const addNameTextbox3 = document.getElementById('add-position');
  const addNameTextbox4 = document.getElementById('add-command_id');
  
  const item = {
    first_name: document.getElementById('add-first_name').value.trim(),
    last_name: document.getElementById('add-last_name').value.trim(),
    position: document.getElementById('add-position').value.trim(),
    command_id: document.getElementById('add-command_id').value.trim()
  };

  fetch(uri, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  })
    .then(response => response.json())
    .then(() => {
      getItems();
      addNameTextbox1.value = '';
      addNameTextbox2.value = '';
      addNameTextbox3.value = '';
      addNameTextbox4.value = '';
    })
    .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
  fetch(`${uri}/${id}`, {
    method: 'DELETE'
  })
  .then(() => getItems())
  .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
  const item = todos.find(item => item.id === id);
  document.getElementById('edit-id').value = item.id;
  document.getElementById('edit-first_name').value = item.first_name; 
  document.getElementById('edit-last_name').value = item.last_name;
  document.getElementById('edit-position').value = item.position;
  document.getElementById('edit-command_id').value = item.command_id;
  document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
  const itemId = document.getElementById('edit-id').value;
  const item = {
    first_name: document.getElementById('edit-first_name').value.trim(),
    last_name: document.getElementById('edit-last_name').value.trim(),
    position: document.getElementById('edit-position').value.trim(),
    command_id: document.getElementById('edit-command_id').value.trim()
  };




  fetch(`${uri}/${itemId}`, {
    method: 'PUT',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  })
  .then(() => getItems())
  .catch(error => console.error('Unable to update item.', error));

  closeInput();

  return false;
}

function closeInput() {
  document.getElementById('editForm').style.display = 'none';
}






//          geros


function _displayCount(itemCount) {
  const name = (itemCount === 1) ? 'player' : 'players';

  document.getElementById('counter').innerText = `Found ${itemCount} ${name}`;
}


function _displayItems(data) {
  const tBody = document.getElementById('todos');
  tBody.innerHTML = '';

  _displayCount(data.length);

  const button = document.createElement('button');

  data.forEach(item => {
    let isCompleteCheckbox = document.createElement('input');
    isCompleteCheckbox.type = 'checkbox';
    isCompleteCheckbox.disabled = true;
    isCompleteCheckbox.checked = item.isComplete;

    let editButton = button.cloneNode(false);
    editButton.innerText = 'Edit';
    editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = 'Delete';
    deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

    let tr = tBody.insertRow();
    
    let td1 = tr.insertCell(0);
    //td1.appendChild(isCompleteCheckbox);
    let textNode = document.createTextNode(item.id);
    td1.appendChild(textNode);

    let td2 = tr.insertCell(1);
    let textNode1 = document.createTextNode(item.first_name);
    td2.appendChild(textNode1);

    let td3 = tr.insertCell(2);
    let textNode2 = document.createTextNode(item.last_name);
    td3.appendChild(textNode2);

    let td4 = tr.insertCell(3);
    let textNode3 = document.createTextNode(item.position);
    td4.appendChild(textNode3);

    let td5 = tr.insertCell(4);
    let textNode4 = document.createTextNode(item.command_id);
    td5.appendChild(textNode4);

    let td6 = tr.insertCell(5);
    td6.appendChild(editButton);

    let td7 = tr.insertCell(6);
    td7.appendChild(deleteButton);
  });

  todos = data;
}
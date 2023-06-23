<script setup lang="ts">
import { ref } from 'vue';

let ws_server = ref ('wss://localhost:4444');

let ws_server_status = ref ('Idle');

let ws_message_to_server = ref ('{ "data" : "Welcome to system" }');

let ws_result = ref ('No result yet');

function do_ws()
{
  console.log(`Connecting to ${ws_server.value}`);
  const socket = new WebSocket(ws_server.value);

  // Connection opened
  socket.addEventListener("open", (event) => {
    ws_server_status.value = "Connected";
    socket.send(ws_message_to_server.value);
  });

  socket.addEventListener("error", (event) => {
    ws_server_status.value = `Disconnected`;
  });

  // Listen for messages
  socket.addEventListener("message", (event) => {
    console.log("Message from server ", event.data);
    ws_result.value = event.data;
  });
}

async function do_fetch()
{
  console.log(`Connecting to ${fetch_server.value}`);

  const r = await fetch(fetch_server.value);
  const v = await r.text();
  fetch_result.value = v;
}


let fetch_server = ref ('http://localhost:5000/');

let fetch_message_to_server = ref ('{ "data" : "Welcome to system" }');

let fetch_result = ref ('No result yet');

</script>

<template>
  <header>
    <div>Demo</div>
  </header>

  <main>
    <h3>Websocket test</h3>
    <div>
      Server:
      <input v-model="ws_server" type=text />
      {{ ws_server_status }}
    </div>
    <div>
      Message to be sent:<br/>
      <textarea v-model="ws_message_to_server" />
    </div>
    <button @click="do_ws()">Connect to the server and send the message</button>
    <br/>
    <div>Result:</div>
    <textarea v-model="ws_result" />

    <h3>Fetch test</h3>
    <div>
      Server:
      <input v-model="fetch_server" type=text />
    </div>
    <div>
      Message to be sent:<br/>
      <textarea v-model="fetch_message_to_server" />
    </div>
    <button @click="do_fetch()">Fetch URL from the server</button>
    <br/>
    <div>Result:</div>
    <textarea v-model="fetch_result" />
  </main>
</template>

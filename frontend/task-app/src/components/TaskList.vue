<template>
    <div class="container my-5">
      <div class="card shadow">
        <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
          <h3 class="mb-0">Task Manager</h3>
          <button class="btn btn-light btn-sm" @click="fetchTasks">Refrescar</button>
        </div>
        <div class="card-body">
          <!-- Task Input -->
          <div class="input-group mb-3">
            <input
              v-model="newTask"
              type="text"
              class="form-control"
              placeholder="Inserta una nueva tarea"
            />
            <button class="btn btn-primary" @click="addTask">Añadir tarea</button>
          </div>
  
          <!-- Task List -->
          <ul class="list-group">
            <li
              v-for="task in tasks"
              :key="task.id"
              class="list-group-item d-flex justify-content-between align-items-center"
            >
              <div>
                <input
                  type="checkbox"
                  class="form-check-input me-2"
                  v-model="task.isCompleted"
                  @change="updateTask(task)"
                />
                <span :class="{ 'text-decoration-line-through': task.isCompleted }">
                  {{ task.title }}
                </span>
              </div>
              <button class="btn btn-danger btn-sm" @click="deleteTask(task.id)">
                Delete
              </button>
            </li>
          </ul>
  
          <!-- No Tasks -->
          <div v-if="tasks.length === 0" class="text-center mt-3 text-muted">
            No hay tareas disponibles. Empieza agregando una nueva
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  import axios from "axios";
  
  export default {
    data() {
      return {
        tasks: [],
        newTask: "",
      };
    },
    methods: {
      fetchTasks() {
        axios
          .get("https://localhost:5001/api/tasks")
          .then((response) => {
            this.tasks = response.data;
          })
          .catch((error) => {
            console.error("Error fetching tasks:", error);
          });
      },
      addTask() {
        if (!this.newTask.trim()) {
          alert("El titulo de la tarea no puede estar vacio");
          return;
        }
  
        axios
          .post("https://localhost:5001/api/tasks", {
            title: this.newTask,
            isCompleted: false,
          })
          .then((response) => {
            this.tasks.push(response.data);
            this.newTask = "";
          })
          .catch((error) => {
            console.error("Error agregando tarea:", error);
          });
      },
      updateTask(task) {
        axios
          .put(`https://localhost:5001/api/tasks/${task.id}`, task)
          .catch((error) => {
            console.error("Error actualizando tarea:", error);
          });
      },
      deleteTask(id) {
        axios
          .delete(`https://localhost:5001/api/tasks/${id}`)
          .then(() => {
            this.tasks = this.tasks.filter((task) => task.id !== id);
          })
          .catch((error) => {
            console.error("Error eliminando tarea", error);
          });
      },
    },
    mounted() {
      this.fetchTasks();
    },
  };
  </script>
  
  <style scoped>
  /* Personalización adicional */
  .text-decoration-line-through {
    color: gray;
  }
  </style>
  
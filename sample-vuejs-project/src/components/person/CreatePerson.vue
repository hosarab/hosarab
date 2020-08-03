<template>
  <div class="submit-form">
    <div v-if="!submitted">
      <div class="form-group">
        <label for="name">Name</label>
        <input
          id="name"
          v-model="person.Name"
          type="text"
          class="form-control"
          required
          name="name"
        >
      </div>
      <!--
      <div class="form-group">
        <label for="createddate">CreatedDate</label>
        <input
          class="form-control"
          id="createddate"
          required
          v-model="person.CreatedDate"
          name="createddate"
        >
      </div>-->

      <button
        class="btn btn-success"
        @click="saveTutorial">Submit</button>
    </div>

    <div v-else>
      <h4>You submitted successfully!</h4>
      <button
        class="btn btn-success"
        @click="newTutorial">Add</button>
    </div>
  </div>
</template>

<script>
/*eslint-disable */
import axios from 'axios';
import PersonService from '@/api-services/persons.service';
export default {
  name: 'CreatePerson',
  data() {
    return {
      person: {
        Name: ""
      }
    };
  },
  methods: {
    saveTutorial() {
      var data = {
        name: this.person.Name,
      };

      PersonService.create(data)
        .then((response) => {
          this.person = response.data.id;
          console.log(JSON.stringify(this.person));
        })
        .catch((e) => {
          console.log(e);
        });
    }
  }
};
</script>

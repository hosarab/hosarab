<template>
  <div>
    <b-row>
      <b-col
        md="2"
        offset-md="10">
        <b-link :to="'createperson'">Add New Person</b-link>
      </b-col>
    </b-row>
    <br>
    <b-row>
      <b-col md="12">
        <div class="table-responsive">
          <table class="table table-striped">
            <thead>
              <tr>
                <th>person name</th>
                <th>CreatedDate</th>
                <th>Details</th>
                <th>Update</th>
                <th>Delete</th>
              </tr>
            </thead>
            <tbody>
              <tr
                v-for="person in persons"
                :key="person.id">
                <td>{{ person.name }}</td>
                <td>{{ person.createdDate }}</td>
                <td>
                  <b-button variant="default">Details</b-button>
                </td>
                <td>
                  <b-button variant="success">Update</b-button>
                </td>
                <td>
                  <b-button variant="danger">Delete</b-button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </b-col>
    </b-row>
  </div>
</template>
<script>
import PersonService from '@/api-services/persons.service';

export default {
  name: 'PersonList',
  data() {
    return {
      persons: []
    };
  },
  created() {
    PersonService.getAll().then((response) => {
      this.persons = response.data;
    });
  }
};
</script>

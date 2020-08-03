<template>
  <p class="homeText">WELCOME TO CODING TASK APPLICATION</p>
</template>
<script>
import PersonService from '@/api-services/persons.service';

export default {
  name: 'Home',
  created() {
    PersonService.getAll().then((response) => {
      console.log(response.data);
    }).catch((error) => {
      console.log(error.response.data);
    });
  }
};
</script>
<style scoped>
.homeText {
  font-size: 35px;
  color: GREEN;
  text-align: center;
  position: relative;
  top: 30px;
  text-shadow: 2px 2px 2px gray;
}
</style>

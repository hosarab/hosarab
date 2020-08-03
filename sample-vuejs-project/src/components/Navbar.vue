<template>
  <b-navbar
    toggleable="md"
    type="dark"
    variant="info">
    <b-navbar-brand :to="{ name: 'Home' }">My Coding Task Logo</b-navbar-brand>
    <b-navbar-brand :to="{ name: 'PersonList' }">People</b-navbar-brand>
    <b-navbar-nav>
      <b-nav-item :to="{ name: 'NotFound' }">NotPage</b-nav-item>
    </b-navbar-nav>
  </b-navbar>
</template>
<script>
export default {
  name: 'Navbar'
};
</script>

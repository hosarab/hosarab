import Vue from 'vue';
import Router from 'vue-router';
import Home from '@/components/Home';
import PersonList from '@/components/person/PersonList';
import NotFound from '@/components/error-pages/NotFound';
import CreatePerson from '@/components/person/CreatePerson';

Vue.use(Router);

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Home
    },
    {
      path: '/person/PersonList',
      name: 'PersonList',
      component: PersonList
    },
    {
      path: '/person/CreatePerson',
      name: 'CreatePerson',
      component: CreatePerson
    },
    {
      path: '*',
      name: 'NotFound',
      component: NotFound
    }
  ]
});

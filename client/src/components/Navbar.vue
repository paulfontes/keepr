<script setup>
import { ref, watch } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';

const theme = ref(loadState('theme') || 'light')


watch(theme, () => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
  saveState('theme', theme.value)
}, { immediate: true })

</script>

<template>
  <nav class="navbar navbar-expand-md bg-primary border-navbar border-secondary">
    <div class="container-fluid gap-4">
      <RouterLink :to="{ name: 'Home' }" class="d-flex align-items-center">
        <b class="ms-5 fs-5 selectable">Home</b>
      </RouterLink>
      <div class="col-md-3">
        <b class="fs-5 selectable">Create<i class="mdi mdi-triangle-small-down"></i></b>
      </div>
      <!-- collapse button -->
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbar-links"
        aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
        <span class="mdi mdi-menu text-light"></span>
      </button>
      <div class=" d-flex justify-content-center col-md-3">
        <div class="the-keeper-co">
          <p class="mb-0">the</p>
          <p class="mb-0">keepr</p>
          <p class="mb-0">co.</p>
        </div>
      </div>
      <!-- collapsing menu -->
      <div class="collapse navbar-collapse " id="navbar-links">
        <ul class="navbar-nav">
          <li>
            <!-- <RouterLink :to="{ name: 'About' }" class="btn text-green selectable">
              About
            </RouterLink> -->
          </li>
        </ul>
        <!-- LOGIN COMPONENT HERE -->
        <div class="ms-auto">

        </div>
        <Login />
      </div>
    </div>
  </nav>
</template>

<style lang="scss" scoped>
a {
  text-decoration: none;
  color: unset;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-secondary);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.the-keeper-co {
  border: 2px solid black;
  width: 75px;
  border-radius: 8%;
  padding: 0px 5px;
}

.border-navbar {
  border-bottom: 7px solid;
  margin-bottom: 30px;
}
</style>

<script setup>
import { Modal } from 'bootstrap';
import Navbar from './components/Navbar.vue';
import { RouterView } from 'vue-router';
import ModalWrapper from './components/ModalWrapper.vue';
import KeepCard from './components/KeepCards.vue';
import { computed } from 'vue';
import { AppState } from './AppState.js';
import KeepDetails from './components/KeepDetails.vue';



</script>

<template>
  <header>
    <Navbar />
  </header>
  <main>
    <RouterView />
  </main>
  <footer class=" text-center">
  </footer>
  <ModalWrapper modalId="keep-details">
    <KeepDetails />
  </ModalWrapper>
</template>

<style lang="scss"></style>

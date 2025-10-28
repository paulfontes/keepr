<script setup>
import { Modal } from 'bootstrap';
import Navbar from './components/Navbar.vue';
import { RouterView } from 'vue-router';
import ModalWrapper from './components/ModalWrapper.vue';
import KeepCard from './components/KeepCards.vue';
import { computed } from 'vue';
import { AppState } from './AppState.js';
import KeepDetails from './components/KeepDetails.vue';
import CreateKeep from './components/CreateKeep.vue';
import CreateVault from './components/CreateVault.vue';
import VaultKeepCard from './components/VaultKeepCards.vue';
import VaultKeepDetails from './components/VaultKeepDetails.vue';



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
  <ModalWrapper modalId="create-keep">
    <CreateKeep />
  </ModalWrapper>
  <ModalWrapper modalId="create-vault">
    <CreateVault />
  </ModalWrapper>
  <ModalWrapper modalId="vault-keep-details">
    <VaultKeepDetails />
  </ModalWrapper>
</template>

<style lang="scss"></style>

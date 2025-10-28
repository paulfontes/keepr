<script setup>
import { AppState } from '@/AppState.js';
import { computed } from 'vue';




const vaults = computed(() => AppState.vaults)
</script>


<template>

    <section class="row">
        <div v-for="vault in vaults" :id="vault.id" class="col-3 position-relative">
            <RouterLink :to="{ name: 'Vault', params: { vaultId: vault.id } }">
                <img :src="vault.img" alt="" class="img-fluid vault-img">
                <h4 class="vault-info">{{ vault.name }}</h4>
                <p v-if="vault.isPrivate" class="is-private-info"><i class="mdi mdi-lock"></i></p>
            </RouterLink>
        </div>

    </section>
</template>


<style lang="scss" scoped>
.delete-button {
    position: absolute;
    top: 10px;

}

.vault-img {
    border-radius: 5px;
    box-shadow: 0px 2px 5px rgb(109, 109, 109);
    width: 100%;
    margin-bottom: 20px;

}

.vault-data {
    position: relative;
}

.vault-info {
    color: white;
    text-shadow: 2px 2px 2px black;
    z-index: 100px;
    position: absolute;
    bottom: 25px;
    left: 20px;
    display: flex;
    justify-content: space-between;
    align-items: center;

}

.is-private-info {
    color: white;
    text-shadow: 2px 2px 2px black;
    z-index: 100px;
    position: absolute;
    bottom: 10px;
    right: 20px;
    display: flex;
    justify-content: space-between;
    align-items: center;
}
</style>

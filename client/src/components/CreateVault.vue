<script setup>
import { AppState } from '@/AppState.js';
import { keepsService } from '@/services/KeepsService.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, ref } from 'vue';


const account = computed(() => AppState.account)

const editableVaultData = ref({
    name: '',
    description: '',
    img: '',
    isPrivate: false
})

async function CreateVault() {
    try {
        const vault = await vaultsService.createVault(editableVaultData.value)
    }
    catch (error) {
        Pop.error(error);
        logger.log('There was an error creating event', error)
    }
}

</script>


<template>
    <form @submit.prevent="CreateVault()">
        <div class="container">
            <section class="row">
                <div class="col-12">
                    <h1>Add your vault</h1>
                </div>
                <div class="col-12">
                    <label for="vault-name">Vault Name</label>
                    <input v-model="editableVaultData.name" required type="text" name="vault-name" id="vault-name"
                        class="form-control">
                </div>
                <div class="col-12">
                    <label for="vault-img">Image URL</label>
                    <input v-model="editableVaultData.img" required type="url" name="vault-img" id="vault-img"
                        class="form-control">
                </div>
                <div class="col-12">
                    <label for="vault-description">Vault Description</label>
                    <textarea v-model="editableVaultData.description" required type="text" name="vault-name"
                        id="vault-name" class="form-control"></textarea>
                </div>
                <div class="col-12 text-end mt-4">
                    <button type="submit" data-bs-dismiss="modal">Create</button>
                </div>
            </section>
        </div>
    </form>
</template>


<style lang="scss" scoped>
textarea {
    height: 100px;
}
</style>
<script setup>
import { AppState } from '@/AppState.js';
import { accountService } from '@/services/AccountService.js';
import { keepsService } from '@/services/KeepsService.js';
import { vaultKeepsService } from '@/services/VaultKeepsService.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';


const activeKeep = computed(() => AppState.activeKeep)
const account = computed(() => AppState.account)
const savedKeeps = computed(() => AppState.savedKeeps)
const vaultKeeps = computed(() => AppState.vaultsKeeps)
const vaults = computed(() => AppState.vaults)

const route = useRoute()


async function saveKeepToVault(activeKeep) {
    try {
        await vaultKeepsService.createVaultKeep(activeKeep)
    }
    catch (error) {
        Pop.error(error);
        logger.log(error)
    }
}



async function getMyVaults() {
    try {
        await accountService.getMyVaults()
    }
    catch (error) {
        Pop.error(error);
        logger.log(error)
    }
}

</script>


<template>
    <div class="container-fluid">
        <section v-if="activeKeep" :key="activeKeep.id" class="row">
            <div class="col-6 p-0">
                <img :src="activeKeep.img" :alt="activeKeep.name" class="img-fluid modal-img">
            </div>
            <div class="col-6 text-center">
                <section class="row">
                    <div class="col-12">
                        <span class="me-3">{{ activeKeep.views }} <i class="mdi mdi-eye"></i></span>
                        <span>10 <i class="mdi mdi-alpha-k fs-3"></i></span>
                    </div>
                    <div class="modal-body">
                        <div class="col-12 mt-5">
                            <h1 class="mb-4">{{ activeKeep.name }}</h1>
                            <p>{{ activeKeep.description }}</p>
                        </div>
                    </div>
                    <div class="d-flex text-end mt-5">
                        <div class="col-6 d-flex justify-content-between">

                            <button v-if="account" @click="getMyVaults()" class="btn btn-secondary dropdown-toggle"
                                type="button" data-bs-toggle="dropdown" aria-expanded="false">
                                Pick a Vault
                            </button>
                            <ul class="dropdown-menu">

                                <div v-for="vault in vaults" :key="vault.id">
                                    <li><a class="dropdown-item">{{ vault.name }}</a></li>
                                </div>
                            </ul>

                            <button v-if="account" @click="saveKeepToVault(activeKeep)">Save</button>
                        </div>
                        <div class="col-6 text-end" data-bs-dismiss="modal">
                            <RouterLink :to="{ name: 'Profile', params: { profileId: activeKeep.creatorId } }">
                                <img :src="activeKeep.creator.picture" alt="" class="modal-profile-pic">
                                {{ activeKeep.creator.name }}
                            </RouterLink>
                        </div>
                    </div>
                </section>
            </div>
        </section>
    </div>
</template>


<style lang="scss" scoped>
.modal-body {
    height: 400px;
    display: flex;
    align-items: center;
}

.modal-profile-pic {
    height: 50px;
    border-radius: 50%;
}

.modal-img {
    height: 550px;
    width: 100%;
    border-radius: 1% 0% 0% 1%;
}

a {
    text-decoration: unset;
    color: unset;

}
</style>
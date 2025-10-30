<script setup>
import { AppState } from '@/AppState.js';
import { accountService } from '@/services/AccountService.js';
import { keepsService } from '@/services/KeepsService.js';
import { vaultKeepsService } from '@/services/VaultKeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';


const activeKeep = computed(() => AppState.activeKeep)
const account = computed(() => AppState.account)
const vaultKeep = computed(() => AppState.vaultKeep)
const savedKeeps = computed(() => AppState.savedKeeps)

const route = useRoute()

async function deleteVaultKeep() {
    const confirmed = await Pop.confirm('Are you sure you want to remove this keep from the vault?')
    if (!confirmed) {
        return
    }
    try {
        const vaultKeepId = activeKeep.value.vaultKeepId
        if (!vaultKeepId) {
            throw new Error('No vaultKeepId found')
        }
        await vaultKeepsService.deleteVaultKeep(vaultKeepId)
        Pop.success('Keep removed from vault')
    }
    catch (error) {
        Pop.error(error);
        logger.log(error)
    }
}

async function getKeepById(keepId) {
    try {
        await keepsService.getKeepById(keepId)

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
            <div class="col-md-6 p-0">
                <img :src="activeKeep.img" :alt="activeKeep.name" class="img-fluid modal-img">
            </div>
            <div class="col-md-6 text-center">
                <section class="row">
                    <div class="col-12">
                        <span class="me-3">{{ activeKeep.views }} <i class="mdi mdi-eye"></i></span>
                        <span>{{ activeKeep.kept }} <i class="mdi mdi-alpha-k fs-3"></i></span>
                    </div>
                    <div class="modal-body">
                        <div class="col-12 mt-5">
                            <h1 class="mb-4">{{ activeKeep.name }}</h1>
                            <p>{{ activeKeep.description }}</p>
                        </div>
                    </div>
                    <div class="d-flex text-end mt-5">
                        <div v-if="vaultKeep?.creatorId == account?.id" class="col-6 d-flex justify-content-between">
                            <button data-bs-dismiss="modal" class="btn btn-outline-red" @click="deleteVaultKeep()"><i
                                    class="mdi mdi-diameter-variant"></i>
                                Remove from Vault</button>
                        </div>
                        <div class="col-md-6 text-end" data-bs-dismiss="modal">
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
    padding: 0;
}

.modal-profile-pic {
    height: 50px;
    border-radius: 50%;
    width: 50px;
    object-fit: cover;
    object-position: top;
}

.modal-img {
    height: 550px;
    width: 100%;
    border-radius: 1% 0% 0% 1%;
    object-fit: cover;
    object-position: center;
}

a {
    text-decoration: unset;
    color: unset;

}
</style>
<script setup>
import { AppState } from '@/AppState.js';
import KeepCards from '@/components/KeepCards.vue';
import VaultKeepCards from '@/components/VaultKeepCards.vue';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted, watch } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const vaultKeep = computed(() => AppState.vaultKeep)
const savedKeeps = computed(() => AppState.savedKeeps)
const account = computed(() => AppState.account)

const route = useRoute()
const router = useRouter()



onMounted(async () => {
    await getVaultById()
})

watch(vaultKeep, async (newVault) => {
    if (newVault) {

        await allSavedKeepsInVault()
    }
})

async function getVaultById() {
    try {
        await vaultsService.getVaultById(route.params.vaultId)
        const vault = AppState.vaultKeep
        if (!vault) {
            throw new Error('Vault not found')

        }

        if (!account.value || (vault.isPrivate && vault.creatorId !== account.value.id)) {
            throw new Error('You do not have access to this vault')
        }
    }
    catch (error) {
        Pop.error(error.message || 'You do not have access to this vault')
        logger.log(error)
        router.push({ name: 'Home' })
    }
}

async function allSavedKeepsInVault() {
    try {
        await vaultsService.savedKeeps(route.params.vaultId)
    }
    catch (error) {

        logger.log(error)
    }
}

</script>


<template>
    <div v-if="vaultKeep" class="container">
        <section class="row justify-content-center">
            <div class="col-5 text-center position-relative">
                <img :src="vaultKeep.img" alt="" class="vault-cover-img ">
                <div class="row text-center">
                    <div class="col-12 position-absolute top-50">
                        <h1 class="vault-info">{{ vaultKeep.name }}</h1>
                    </div>
                    <div class="col-12 position-absolute vault-creator-name">
                        <h5 class="vault-name">by {{ vaultKeep.creator.name }}</h5>
                    </div>
                </div>
            </div>
            <div class="col-12 text-center my-5 d-flex justify-content-center">
                <h4 class="border bg-purple rounded num-keeps">{{ savedKeeps.length }} Keeps</h4>
            </div>
            <div class="col-12 masonry-container">
                <VaultKeepCards v-for="savedKeep in savedKeeps" :key="savedKeep.id" :savedKeep="savedKeep" />
            </div>
        </section>
    </div>
</template>


<style lang="scss" scoped>
.num-keeps {
    width: 120px;
    padding: 3px;
}

.masonry-container {
    columns: 300px;

    >div {
        break-inside: avoid;
    }

}

.vault-creator-name {
    bottom: 25px;
}

.vault-cover-img {
    height: 250px;
    border-radius: 5px;
    box-shadow: 0px 2px 5px rgb(109, 109, 109);
    width: 100%;
    object-fit: cover;
    object-position: center;
}

.vault-info {
    color: white;
    text-shadow: 2px 2px 2px black;
    z-index: 100px;
    // position: absolute;
    // bottom: 60px;
    // left: 150px;
    // display: flex;
    // justify-content: space-between;
    align-items: center;

}

.vault-name {
    // position: absolute;
    color: white;
    text-shadow: 2px 2px 2px black;
    bottom: 20px;
    left: 230px;
}
</style>
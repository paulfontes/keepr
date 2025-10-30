<script setup>
import { AppState } from '@/AppState.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed } from 'vue';




const myVaults = computed(() => AppState.myVaults)
const account = computed(() => AppState.account)


async function deleteVault(vaultId) {
    const confirmed = await Pop.confirm('Are you sure you want to delete this vault? This action cannot be undone.')
    if (!confirmed) {
        return
    }
    try {
        await vaultsService.deleteVault(vaultId)
    }
    catch (error) {
        Pop.error(error);
        logger.log(error)
    }
}



</script>


<template>

    <section class="row">
        <div v-for="myVault in myVaults" :id="myVault.id" class="col-md-3 position-relative">
            <button v-if="account?.id == myVault.creatorId" @click="deleteVault(myVault.id)"
                class="btn btn-outline-red position-absolute"><i class="mdi mdi-delete-circle-outline"
                    title="delete vault"></i></button>
            <RouterLink :to="{ name: 'Vault', params: { vaultId: myVault.id } }">
                <img :src="myVault.img" alt="" class="img-fluid myVault-img">
                <h4 class="myVault-info">{{ myVault.name }}</h4>
                <p v-if="myVault.isPrivate" class="is-private-info"><i class="mdi mdi-lock"></i></p>
            </RouterLink>
        </div>

    </section>
</template>


<style lang="scss" scoped>
.delete-button {
    position: absolute;
    top: 10px;

}

.myVault-img {
    border-radius: 5px;
    box-shadow: 0px 2px 5px rgb(109, 109, 109);
    width: 100%;
    margin-bottom: 20px;
    height: 200px;
    object-fit: cover;
    object-position: center;

}

.myVault-data {
    position: relative;
}

.myVault-info {
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

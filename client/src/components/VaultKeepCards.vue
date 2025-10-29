<script setup>
import { AppState } from '@/AppState.js';
import { computed } from 'vue';
import { Pop } from '@/utils/Pop.js';
import { logger } from '@/utils/Logger.js';
import { keepsService } from '@/services/KeepsService.js';

const props = defineProps({
    savedKeep: {
        type: Object,
        required: true
    }
})

const account = computed(() => AppState.account)

async function getKeepById(keepId) {
    try {
        await keepsService.getKeepById(keepId)
        AppState.activeKeep = { ...AppState.activeKeep, vaultKeepId: props.savedKeep.vaultKeepId }
    }
    catch (error) {
        Pop.error(error);
        logger.log(error)
    }
}
</script>


<template>

    <div class="container" :id="savedKeep.id">
        <div class="keep-data row">
            <img @click="getKeepById(savedKeep.id)" :src="savedKeep.img" class="keep-img p-0" alt=""
                data-bs-toggle="modal" data-bs-target="#vault-keep-details">
            <div class="keep-info d-flex">
                <div>
                    <h5>{{ savedKeep.name }}</h5>
                </div>
                <div v-if="savedKeep?.creator" class="profile-img-location">
                    <img class="profile-img" :src="savedKeep.creator.picture"
                        :alt="'Picture of' + savedKeep.creator.name" :title="savedKeep.creator.name">
                </div>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.delete-button {
    position: absolute;
    top: 10px;

}

.keep-img {
    border-radius: 5px;
    box-shadow: 0px 2px 5px rgb(109, 109, 109);
    width: 100%;
    margin-bottom: 15px;

}

.keep-data {
    position: relative;
}

.keep-info {
    color: white;
    text-shadow: 2px 2px 2px black;
    z-index: 100px;
    position: absolute;
    bottom: 25px;
    left: 5px;
    display: flex;
    justify-content: space-between;
    align-items: center;

}

.profile-img {
    height: 50px;
    border-radius: 50%;
    width: 50px;
    object-fit: cover;
    object-position: top;
}
</style>
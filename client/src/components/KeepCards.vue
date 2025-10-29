<script setup>
import { AppState } from '@/AppState.js';
import { computed } from 'vue';
import ModalWrapper from './ModalWrapper.vue';
import { Pop } from '@/utils/Pop.js';
import { logger } from '@/utils/Logger.js';
import { keepsService } from '@/services/KeepsService.js';


const keeps = computed(() => AppState.keeps)
const activeKeep = computed(() => AppState.activeKeep)
const account = computed(() => AppState.account)


async function deleteKeep(keepId) {
    try {
        await keepsService.deleteKeep(keepId)
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

    <div v-for="keep in keeps" :id="keep.id">
        <div class="container">

            <div class="keep-data row">
                <div class="delete-button">
                    <button v-if="account?.id == keep.creatorId" @click="deleteKeep(keep.id)"
                        class="btn btn-outline-red"><i class="mdi mdi-delete"></i></button>
                </div>
                <img @click="getKeepById(keep.id)" :src="keep.img" class="keep-img p-0" alt="" data-bs-toggle="modal"
                    data-bs-target="#keep-details">
                <div class="keep-info d-flex">
                    <div>
                        <h5>{{ keep.name }}</h5>
                    </div>
                    <div class="profile-img-location">
                        <img class="profile-img" :src="keep.creator.picture" :alt="'Picture of' + keep.creator.name"
                            :title="keep.creator.name">
                    </div>
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
<script setup>
import { AppState } from '@/AppState.js';
import { keepsService } from '@/services/KeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed } from 'vue';
import { useRoute } from 'vue-router';


const activeKeep = computed(() => AppState.activeKeep)
const account = computed(() => AppState.account)


</script>


<template>
    <div class="container-fluid">
        <section v-if="activeKeep" :key="activeKeep.id" class="row">
            <div class="col-6">
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
                        <div class="col-6">
                            <button class="me-5 w-50">Plants</button>
                            <button>Save</button>
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
}

a {
    text-decoration: unset;
    color: unset;

}
</style>
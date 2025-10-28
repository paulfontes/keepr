<script setup>
import { AppState } from '@/AppState.js';
import KeepCards from '@/components/KeepCards.vue';
import VaultCard from '@/components/VaultCard.vue';
import { profilesService } from '@/services/ProfilesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';


const keeps = computed(() => AppState.keeps)
const profile = computed(() => AppState.profile)
const activeKeep = computed(() => AppState.activeKeep)
const vaults = computed(() => AppState.vaults)

const route = useRoute()

onMounted(() => {
    getProfileById()
    getProfileKeeps()
    getProfileVaults()
})

async function getProfileById() {
    try {
        await profilesService.getProfile(route.params.profileId)
    }
    catch (error) {
        Pop.error(error);
        logger.log(error)
    }
}

async function getProfileKeeps() {
    try {
        await profilesService.getProfileKeeps(route.params.profileId)
    }
    catch (error) {
        Pop.error(error);
        logger.log(error)
    }
}

async function getProfileVaults() {
    try {
        await profilesService.getProfileVaults(route.params.profileId)
    }
    catch (error) {
        Pop.error(error);
        logger.log(error)
    }
}

</script>


<template>
    <div v-if="profile" :key="profile.id" class="container">
        <section class="row justify-content-center my-4">
            <div class="col-10">
                <img :src="profile.coverImg" alt="" class="img-fluid cover-img">
            </div>
            <div class="col-12 text-center">
                <img :src="profile.picture" alt="" class="img-fluid profile-img">
            </div>
            <div class="col-12">
                <h2 class="text-center mt-2">{{ profile.name }}</h2>
            </div>
            <div class="col-12 text-center">
                <p> {{ AppState.vaults.length }} Vaults | {{ AppState.keeps.length }} Keeps</p>
            </div>
            <div class="col-12">
                <h1>Vaults</h1>
                <VaultCard />
            </div>
            <div class="col-12">
                <h1>Keeps</h1>
                <div class="masonry-container">
                    <KeepCards />
                </div>
            </div>
        </section>
    </div>
</template>


<style lang="scss" scoped>
.masonry-container {
    columns: 300px;

    >div {
        break-inside: avoid;
    }

}

.cover-img {
    height: 300px;
    border-radius: 5px;
    box-shadow: 0px 2px 5px rgb(109, 109, 109);
    width: 100%;
}

.profile-img {
    border-radius: 50%;
    box-shadow: 0px 2px 5px rgb(109, 109, 109);
    width: 150px;
    margin-top: -75px;
    border: 5px solid white;
}
</style>
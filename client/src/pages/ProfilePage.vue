<script setup>
import { AppState } from '@/AppState.js';
import { profilesService } from '@/services/ProfilesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';


const keeps = computed(() => AppState.keeps)
const profile = computed(() => AppState.profile)
const activeKeep = computed(() => AppState.activeKeep)

const route = useRoute()

onMounted(() => {
    getProfileById()
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
                <p>5 Vaults | 21 Keeps</p>
            </div>
        </section>
    </div>
</template>


<style lang="scss" scoped>
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
<script setup>
import { AppState } from '@/AppState.js';
import KeepCards from '@/components/KeepCards.vue';
import MyVaultCard from '@/components/MyVaultCard.vue';
import VaultCard from '@/components/VaultCard.vue';
import { accountService } from '@/services/AccountService.js';
import { profilesService } from '@/services/ProfilesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted, watch } from 'vue';
import { useRoute } from 'vue-router';


const keeps = computed(() => AppState.keeps)
const profile = computed(() => AppState.profile)
const activeKeep = computed(() => AppState.activeKeep)
const myVaults = computed(() => AppState.myVaults)
const account = computed(() => AppState.account)


const route = useRoute()


onMounted(() => {
    getProfileById()
    getProfileVaults()
    getProfileKeeps()
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
            <div v-if="account && route.params.profileId == account.id" class="col-12 text-center">
                <p> {{ AppState.myVaults.length }} Vaults | {{ AppState.keeps.length }} Keeps</p>
            </div>
            <div v-else class="col-12 text-center">
                <p> {{ AppState.vaults.length }} Vaults | {{ AppState.keeps.length }} Keeps</p>
            </div>
            <div v-if="account && route.params.profileId == account.id" class="col-12">
                <h1>Vaults</h1>
                <MyVaultCard />
            </div>
            <div v-else class="col-12">
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

@media(max-width:584px) {
    .masonry-container {
        columns: 130px;

        >div {
            break-inside: avoid;
        }

    }

}

.cover-img {
    height: 300px;
    border-radius: 5px;
    box-shadow: 0px 2px 5px rgb(109, 109, 109);
    width: 100%;
    object-fit: cover;
}

.profile-img {
    border-radius: 50%;
    box-shadow: 0px 2px 5px rgb(109, 109, 109);
    height: 150px;
    width: 150px;
    margin-top: -75px;
    border: 5px solid white;
    object-position: top;
    object-fit: cover;
}
</style>
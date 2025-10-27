<script setup>
import { AppState } from '@/AppState.js';
import { profilesService } from '@/services/ProfilesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed } from 'vue';




const keeps = computed(() => AppState.keeps)
const account = computed(() => AppState.account)

</script>


<template>
    <div class="container">
        <section class="row justify-content-center my-4">
            <div class="col-10">
                <img :src="account.coverImg" alt="" class="img-fluid cover-img">
            </div>
            <div class="col-12 text-center">
                <img :src="account.picture" alt="" class="img-fluid profile-img">
            </div>
            <div class="col-12">
                <h2 class="text-center mt-2">{{ account.name }}</h2>
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
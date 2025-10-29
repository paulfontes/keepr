<script setup>
import { computed, ref, watch } from 'vue';
import { AppState } from '../AppState.js';
import { Pop } from '@/utils/Pop.js';
import { logger } from '@/utils/Logger.js';
import { accountService } from '@/services/AccountService.js';

const account = computed(() => AppState.account)


const formData = ref({

  name: '',
  picture: '',
  coverImg: 'https://www.altavia.hu/wp-content/uploads/2020/11/Hero-Banner-Placeholder-Light-1024x480-1.png'

})

watch(account, () => {
  formData.value = { ...account.value }

}, { immediate: true })

async function sendFormData() {
  try {
    await accountService.updateAccount(formData.value)
    Pop.success('Your Profile has been updated!')
  }
  catch (error) {
    Pop.error('Failed to Save Profile Changes!', error);
    logger.log('Failed to Save Profile Changes!', error)
  }
}






</script>

<template>
  <main class="container-fluid back-img">
    <section v-if="account" class="row justify-content-center pt-5">
      <div class="col-4 text-black">
        <h1>{{ account.name }}'s Account</h1>
      </div>
      <div class="col-4">
        <img :src="account.picture" class="profile-pic" alt="">
      </div>
      <div class="col-8 mt-4 text-black">
        <form @submit.prevent="sendFormData()">
          <span class="d-flex gap-5">
            <div class="w-50">
              <label for="account-name">Username:</label>
              <input required id="account-name" name="account-name" v-model="formData.name" type="text"
                class="form-control">
            </div>
            <div class="w-50">
              <label for="account-picture">Profile Picture:</label>
              <input required id="account-picture" name="account-picture" v-model="formData.picture" type="url"
                class="form-control">
            </div>
          </span>
          <div class="mt-4">
            <label for="account-cover">Profile Banner:</label>
            <input id="account-cover" name="account-cover" v-model="formData.coverImg" type="url" class="form-control">
          </div>
          <div class="text-end">
            <button type="submit" class="btn btn-success mt-2">
              Submit
            </button>
          </div>
        </form>
      </div>
      <section class="row">
        <div class="col text-center text-black">
          <h5> Cover Img Preview</h5>
          <img v-if="account.coverImg" class="cover-img my-4" :src="account.coverImg" alt="">
          <img v-else class="cover-img my-4"
            src="https://www.altavia.hu/wp-content/uploads/2020/11/Hero-Banner-Placeholder-Light-1024x480-1.png" alt="">
        </div>
      </section>
    </section>
  </main>
</template>

<style scoped lang="scss">
.profile-pic {
  border-radius: 50%;
  height: 120px;
  aspect-ratio: 1/1;
  object-fit: cover;
  object-position: center;
}

.back-img {
  min-height: 500px;
  background-color: rgb(254 246 240);
  background-position: top;
  background-size: cover;
}

.bio-box {
  height: 200px;
}

.cover-img {
  border-radius: 20px;
  height: 200px;
  width: 67%;
  object-fit: cover;
}
</style>

<script setup>
import { AppState } from '@/AppState.js';
import { keepsService } from '@/services/KeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, ref } from 'vue';


const account = computed(() => AppState.account)

const editableKeepData = ref({
    name: '',
    description: '',
    img: ''
})

async function CreateKeep() {
    try {
        const keep = await keepsService.createKeep(editableKeepData.value)
    }
    catch (error) {
        Pop.error(error);
        logger.log('There was an error creating event', error)
    }
}

</script>


<template>
    <form @submit.prevent="CreateKeep()">
        <div class="container">
            <section class="row">
                <div class="col-12">
                    <h1>Add your keep</h1>
                </div>
                <div class="col-12">
                    <label for="keep-name">Keep Name</label>
                    <input v-model="editableKeepData.name" required type="text" name="keep-name" id="keep-name"
                        class="form-control">
                </div>
                <div class="col-12">
                    <label for="keep-img">Image URL</label>
                    <input v-model="editableKeepData.img" required type="url" name="keep-img" id="keep-img"
                        class="form-control">
                </div>
                <div class="col-12">
                    <label for="keep-tag">Keep Tags</label>
                    <input type="text" name="keep-tag" id="keep-tag" class="form-control">
                </div>
                <div class="col-12">
                    <label for="keep-description">Keep Description</label>
                    <textarea v-model="editableKeepData.description" required type="text" name="keep-name"
                        id="keep-name" class="form-control"></textarea>
                </div>
                <div class="col-12 text-end mt-4">
                    <button type="submit" data-bs-dismiss="modal">Create</button>
                </div>
            </section>
        </div>
    </form>
</template>


<style lang="scss" scoped>
textarea {
    height: 100px;
}
</style>
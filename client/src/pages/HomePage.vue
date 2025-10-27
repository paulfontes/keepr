<script setup>
import { AppState } from '@/AppState.js';
import Example from '@/components/Example.vue';
import KeepCard from '@/components/KeepCard.vue';
import { keepsService } from '@/services/KeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';


const keeps = computed(() => AppState.keeps)

onMounted(() => {
  getKeeps()
})

async function getKeeps() {
  try {
    await keepsService.getKeeps()
  }
  catch (error) {
    Pop.error(error);
    logger.log(error)
  }
}
</script>

<template>
  <div class="container">
    <section class="row">
      <div class="masonry-container col-12">
        <KeepCard />
      </div>
    </section>
  </div>
</template>

<style scoped lang="scss">
.masonry-container {
  columns: 300px;

  >div {
    break-inside: avoid;
  }

}
</style>

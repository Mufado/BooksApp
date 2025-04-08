<template>
  <div class="p-4">
    <input v-model="filter" placeholder="Filter by title..." class="mb-4 p-2 border rounded" />

    <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
      <div v-for="book in filteredBooks" :key="book.id" class="p-4 border rounded shadow">
        <h2 class="text-lg font-bold">{{ book.title }}</h2>
        <p class="text-sm italic" v-if="book.subtitle">{{ book.subtitle }}</p>
        <p class="text-sm text-gray-600 mt-2">{{ book.categories.join(', ') }}</p>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref, computed } from 'vue';
import { getBooks, Book } from '@/services/bookService.ts';

export default defineComponent({
  setup() {
    const books = ref<Book[]>([]);
    const filter = ref('');

    const filteredBooks = computed(() => {
      return books.value.filter(b =>
        b.title.toLowerCase().includes(filter.value.toLowerCase())
      );
    });

    //onMounted(async () => {
    //  books.value = await getBooks();
    //});

    return { books, filter, filteredBooks };
  },
});
</script>

<style scoped>
  /* Opcional: estilos adicionais */
</style>

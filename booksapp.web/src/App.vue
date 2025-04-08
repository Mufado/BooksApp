<template>
  <ag-grid-vue class="ag-theme-alpine"
                style="width: 80vw; height: 80vh;"
                :columnDefs="columnDefs"
                :rowData="rowData"
                :defaultColDef="defaultColumnDef"
                :dataTypeDefinitions="dataTypeDefinitions"/>
</template>

<script setup lang="ts">
  import { onMounted, ref } from 'vue';
  import { AgGridVue } from 'ag-grid-vue3';
  import { AllCommunityModule, ModuleRegistry } from 'ag-grid-community';

  ModuleRegistry.registerModules([AllCommunityModule]);

  defineOptions({
    name: 'BooksApp',
    components: {
      AgGridVue,
    }
  });

  const dataTypeDefinitions = ref({
    date: {
      baseDataType: 'date',
      extendsDataType: 'date',
      valueFormatter: (params: any) => {
        if (params.value == null) {
          return '';
        }

        const formattedDate = params.value.toLocaleString({
          day: '2-digit',
          month: '2-digit',
          year: 'numeric',
          hour: '2-digit',
          minute: '2-digit',
          second: '2-digit',
          hour12: false
        });
        return formattedDate;
      }
    }
  });

  const columnDefs = ref([
    { field: 'id' },
    { field: 'googleBooksId' },
    { field: 'title' },
    { field: 'subtitle' },
    { field: 'pageCount' },
    { field: 'mainCategory' },
    { field: 'categories' },
    { field: 'height', cellDataType: "number" },
    { field: 'width', cellDataType: "number" },
    { field: 'thickness', cellDataType: "number" },
    { field: 'averageRating' },
    { field: 'ratingsCount' },
    { field: 'creationDate', cellDataType: "date" },
    { field: 'lastModificationDate', cellDataType: "date" }
  ]);

  const defaultColumnDef = ref({
    filter: true
  });

  const rowData = ref([]);

  onMounted(async () => {
    rowData.value = await fetchData();
  });

  const fetchData = async () => {
    const response = await fetch('https://localhost:7161/books/all');
    return (await response.json()).map((book: any) => ({
      id: book.id,
      googleBooksId: book.googleBooksId,
      title: book.title,
      subtitle: book.subTitle,
      pageCount: book.pageCount,
      mainCategory: book.mainCategory,
      categories: book.categories,
      height: book.dimensions?.height != null ? parseFloat(book.dimensions.height) : null,
      width: book.dimensions?.width != null ? parseFloat(book.dimensions.width) : null,
      thickness: book.dimensions?.thickness != null ? parseFloat(book.dimensions.thickness) : null,
      averageRating: book.averageRating,
      ratingsCount: book.ratingsCount,
      creationDate: book.creationDate ? new Date(book.creationDate) : null,
      lastModificationDate: book.lastModificationDate ? new Date(book.lastModificationDate) : null,
    }));
  };
</script>

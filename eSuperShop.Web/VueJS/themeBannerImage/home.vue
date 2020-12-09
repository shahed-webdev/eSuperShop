<template>
    <div class="mt-5">
        <h3 class="mb-3">theme</h3>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                data: [],
                params: { Page: 2, PageSize: 4 },
                isLastPage: false,
                isLoading: false
            }
        },
        methods: {
            getData() {
                axios.get('/home/GetMoreToLove', { params: { Page: 1, PageSize: 4 } }).then(response => {
                    const { IsSuccess, Data } = response.data;
                    if (!IsSuccess) return;

                    this.data = Data.Results;
                });
            },

            loadMore() {
                if (this.isLastPage) return;
                this.isLoading = true;

                axios.get('/home/GetMoreToLove', { params: this.params }).then(response => {
                    const { IsSuccess, Data } = response.data;

                    this.isLastPage = Data.Results.length? false : true;
                    this.isLoading = false;

                    if (!IsSuccess) return;

                    this.data.push(...Data.Results);
                    this.params.Page++;
                }).catch(err => {
                    console.log(err);
                    this.isLoading = false;
                });
            },
        },
        beforeMount() {
            //this.getData();
        }
    }
</script>
<template>
    <div>
        <h4 class="category-title mb-4">THE CATEGORY</h4>

        <div class="row">
            <div class="col-lg-4 col-6 mb-4">
                <div class="card">
                    <div class="view overlay">
                        <img class="card-img-top" src="/images/men/1.jpg" alt="">
                        <a href="#!">
                            <div class="mask rgba-white-slight"></div>
                        </a>
                    </div>

                    <div class="product-name">
                        <p class="mb-0">Name of the product</p>
                        <strong>৳500</strong>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 col-6 mb-4">
                <div class="card">
                    <div class="view overlay">
                        <img class="card-img-top" src="/images/men/2.jpg" alt="">
                        <a href="#!">
                            <div class="mask rgba-white-slight"></div>
                        </a>
                    </div>

                    <div class="product-name">
                        <p class="mb-0">Name of the product</p>
                        <strong>৳500</strong>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 col-6 mb-4">
                <div class="card">
                    <div class="view overlay">
                        <img class="card-img-top" src="/images/men/3.jpg" alt="">
                        <a href="#!">
                            <div class="mask rgba-white-slight"></div>
                        </a>
                    </div>

                    <div class="product-name">
                        <p class="mb-0">Name of the product</p>
                        <strong>৳500</strong>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-4 col-6 mb-4">
                <div class="card">
                    <div class="view overlay">
                        <img class="card-img-top" src="/images/men/2.jpg" alt="">
                        <a href="#!">
                            <div class="mask rgba-white-slight"></div>
                        </a>
                    </div>

                    <div class="product-name">
                        <p class="mb-0">Name of the product</p>
                        <strong>৳500</strong>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 col-6 mb-4">
                <div class="card">
                    <div class="view overlay">
                        <img class="card-img-top" src="/images/men/3.jpg" alt="">
                        <a href="#!">
                            <div class="mask rgba-white-slight"></div>
                        </a>
                    </div>

                    <div class="product-name">
                        <p class="mb-0">Name of the product</p>
                        <strong>৳500</strong>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 col-6 mb-4">
                <div class="card">
                    <div class="view overlay">
                        <img class="card-img-top" src="/images/men/1.jpg" alt="">
                        <a href="#!">
                            <div class="mask rgba-white-slight"></div>
                        </a>
                    </div>

                    <div class="product-name">
                        <p class="mb-0">Name of the product</p>
                        <strong>৳500</strong>
                    </div>
                </div>
            </div>
        </div>
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
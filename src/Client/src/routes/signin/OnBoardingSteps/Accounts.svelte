<script>
	import { fade } from 'svelte/transition';
</script>

<div class="flex h-full w-full flex-col px-20" in:fade>
	<h1 class="mt-1 max-w-lg text-left text-4xl font-medium text-white">
		Manage multiple accounts in one place!
	</h1>
	<p class="mt-2">
		You can manage all of your social media accounts in one convenient location. No more logging in
		and out of multiple platforms, wasting time and energy.
	</p>

	<div class="wrapper relative mx-auto mt-10 h-full w-full">
		<div class="account left-0 h-28 w-28 border-green-300 bg-green-300">
			<div class="img-container">
				<span class="animate-pulse bg-green-300" />
				<img
					src="https://images.unsplash.com/photo-1597075095400-fb3f0de70140?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=689&q=80"
					alt="tiktok"
				/>
			</div>
		</div>
		<div class="account h-36 w-36 border-green-300 bg-green-300">
			<div class="img-container">
				<span class="animate-pulse bg-purple-400" />
				<img
					src="https://images.unsplash.com/photo-1601141586963-f213d2575b7f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Nnx8ZmFjZWJvb2t8ZW58MHx8MHx8&auto=format&fit=crop&w=500&q=60"
					alt="person"
				/>
			</div>
		</div>
		<div class="account right-0 top-4 h-24 w-24 border-green-300 bg-green-300">
			<div class="img-container">
				<span class="animate-pulse bg-pink-400" />
				<img
					src="https://images.unsplash.com/photo-1594670297948-e910d5964979?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8NXx8ZmFjZWJvb2t8ZW58MHx8MHx8&auto=format&fit=crop&w=500&q=60"
					alt="person"
				/>
			</div>
		</div>
		<div class="account h-24 w-24 border-green-300 bg-green-300">
			<div class="img-container">
				<span class=" animate-pulse bg-blue-400" />
				<img
					src="https://images.unsplash.com/photo-1552058544-f2b08422138a?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Nnx8cGVyc29ufGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=500&q=60"
					alt="person"
				/>
			</div>
		</div>

		<div class="account h-28 w-28 border-green-300 bg-green-300">
			<div class="img-container">
				<span class="animate-pulse bg-purple-400" />
				<img
					src="https://images.unsplash.com/photo-1491349174775-aaafddd81942?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MTF8fHBlcnNvbnxlbnwwfHwwfHw%3D&auto=format&fit=crop&w=500&q=60"
					alt="person"
				/>
			</div>
		</div>
		<div class="account h-24 w-24 border-green-300 bg-green-300">
			<div class="img-container">
				<span class="animate-pulse bg-yellow-400" />
				<img
					src="https://images.unsplash.com/photo-1504593811423-6dd665756598?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MTh8fHBlcnNvbnxlbnwwfHwwfHw%3D&auto=format&fit=crop&w=500&q=60"
					alt="person"
				/>
			</div>
		</div>
		<div class="account h-32 w-32 border-green-300 bg-green-300">
			<div class="img-container">
				<span class="animate-pulse bg-pink-400" />
				<img
					src="https://images.unsplash.com/photo-1554151228-14d9def656e4?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8OXx8cGVyc29ufGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=500&q=60"
					alt="person"
				/>
			</div>
		</div>

		<div class="account h-36 w-36 border-green-300 bg-green-300">
			<div class="img-container">
				<span class=" animate-pulse bg-green-300" />
				<img
					src="https://images.unsplash.com/photo-1438761681033-6461ffad8d80?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8cGVyc29ufGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=500&q=60"
					alt="person"
				/>
			</div>
		</div>
		<div class="account h-28 w-28 border-green-300 bg-green-300">
			<div class="img-container">
				<span class=" animate-pulse bg-green-500" />
				<img
					src="https://images.unsplash.com/photo-1473830394358-91588751b241?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80"
					alt="person"
				/>
			</div>
		</div>

		<div class="account h-24 w-24 border-green-300 bg-green-300 ">
			<div class="img-container">
				<span class="animate-pulse bg-green-500" />
				<img
					src="https://images.unsplash.com/photo-1506748686214-e9df14d4d9d0?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MTB8fHBlcnNvbnxlbnwwfHwwfHw%3D&auto=format&fit=crop&w=500&q=60"
					alt="person"
				/>
			</div>
		</div>
	</div>
</div>

<style lang="postcss">
	p {
		max-width: 80%;
	}

	.wrapper {
		max-width: 60%;
	}

	.img-container {
		width: 100%;
		height: 100%;
		position: relative;
	}

	.img-container img {
		position: relative;
		z-index: 2;
	}

	.img-container span {
		width: 100%;
		height: 100%;
		position: absolute;
		border-radius: 100%;
		filter: blur(theme('blur.lg'));
		content: '';
		z-index: 1;
	}

	.account {
		border-radius: 100%;
		border-width: theme('borderWidth.2');
		position: absolute;
	}

	.account img {
		width: 100%;
		height: 100%;
		border-radius: 100%;
		object-fit: cover;
	}

	.account:nth-of-type(2) {
		right: 45%;
		transform: translateX(50%);
	}

	.account:nth-of-type(4) {
		top: 7rem;
		left: 6rem;
	}

	.account:nth-of-type(5),
	.account:nth-of-type(6) {
		top: 14rem;
	}

	.account:nth-of-type(6) {
		left: 11rem;
	}

	.account:nth-of-type(7) {
		right: 2rem;
		top: 9rem;
	}

	.account:nth-of-type(8) {
		bottom: 7rem;
		left: 5rem;
	}

	.account:nth-of-type(9) {
		bottom: 11rem;
		right: 6rem;
	}

	.account:nth-of-type(10) {
		transform: translateX(60%);
		right: 42%;
		bottom: 3rem;
	}
</style>

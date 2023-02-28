<script lang="ts">
	import SubmitButton from '$lib/components/buttons/SubmitButton.svelte';
	import Checkbox from '$lib/components/Checkbox.svelte';
	import Divider from '$lib/components/Divider.svelte';
	import Head from '$lib/components/Head.svelte';
	import LambdaIcon from '$lib/components/icons/LambdaIcon.svelte';
	import Input from '$lib/components/Input.svelte';
	import GoogleOauthLink from '$lib/components/links/GoogleOauthLink.svelte';
	import AuthOnBoarding from '$lib/components/onBoardings/AuthOnBoarding.svelte';

	import { SignInViewModel } from '$lib/viewModels/SignInViewModel';
	import Accounts from './OnBoardingSteps/Accounts.svelte';
	import Uploading from './OnBoardingSteps/Uploading.svelte';
	import Welcome from './OnBoardingSteps/Welcome.svelte';

	let credentials = new SignInViewModel();

	const toggleRememberMe = () => (credentials.remember_me = !credentials.remember_me);

	const handleSubmit = (event: any) => {
		const formData = new FormData(event.target);
		for (let field of formData) {
			const [key, value] = field;
		}
	};
</script>

<Head
	title="Sign in"
	description="Sign in to GoHorse to access your account and enjoy all the features and benefits of our platform. Enter your username and password to securely log in and stay connected with our community. Don't have an account yet? Sign up now and join the GoHorse family!"
	thumb="/signin.png"
/>

<main class="w-full p-8">
	<div class="flex h-full w-full items-center justify-center xl:grid">
		<AuthOnBoarding
			steps={[
				{
					component: Welcome
				},
				{
					component: Uploading
				},
				{
					component: Accounts
				}
			]}
			className="hidden xl:flex"
		/>
		<div class="flex flex-col items-start xl:mx-auto">
			<LambdaIcon className="xl:hidden" />
			<h2 class="mt-10 text-3xl font-bold text-white xl:mt-0">Sign in</h2>
			<p class="leading-lg mt-4 text-lg">
				You can login with your registered account or quick login with your Google account.
			</p>

			<GoogleOauthLink title="Login with Google" className="mt-6 mb-4" />

			<Divider><p class="px-2 font-semibold">Or</p></Divider>

			<form class="mt-4 flex h-max w-full flex-col" on:submit|preventDefault={handleSubmit}>
				<Input name="email" label="Email" />
				<Input className="mt-7" name="password" label="Password" type="password">
					<a class="link absolute right-0 top-0" href="/forgot">Forgot yout password?</a>
				</Input>

				<Checkbox
					className="my-7"
					label="Remember me"
					isChecked={credentials.remember_me}
					onChange={toggleRememberMe}
				/>

				<SubmitButton>Login</SubmitButton>
			</form>

			<div class="mt-14 flex w-full items-center justify-center">
				<p class="mr-2 text-base font-semibold">Don't have an account?</p>
				<a class="link" href="/signup">Create one! </a>
			</div>
		</div>
	</div>
</main>

<style lang="postcss">
	main {
		height: 100vh;
		width: 100%;
	}

	.link {
		color: theme('colors.primary');
		font-family: Inter;
		font-weight: theme('fontWeight.semibold');
	}

	@media (min-width: 1280px) {
		main > div {
			grid-template-columns: 0.8fr 1fr;
		}

		main > div > div:last-of-type {
			width: 50%;
		}
	}
</style>

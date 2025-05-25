import { useState } from "react";
import { Link } from "react-router-dom";

import { useAuth } from "@/context/AuthContext";

import {
	Box,
	Button,
	Container,
	TextField,
	Typography,
	Paper,
	Alert,
	Link as MuiLink,
	CircularProgress,
} from '@mui/material';


const Register = () => {
    const [name, setName] = useState<string>('');
    const [username, setUsername] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    
    const [isLoading, setIsLoading] = useState<boolean>(false);
	const [error, setError] = useState<string>('');

	const { register } = useAuth();

    const handleSubmit = async (e: React.FormEvent) => {
        try{
			setIsLoading(true);
			
			e.preventDefault();
        	await register(name, username, password);
		}
		catch(error){
			if (error instanceof Error) {
				setError(error.message);
			}
			else {
				setError('Erro desconhecido');
			}
		}
		finally{
			setIsLoading(false);
		}
    };
  
    return(
        <Box
			sx={{
				minHeight: '100vh',
				display: 'flex',
				alignItems: 'center',
				justifyContent: 'center',
				backgroundColor: '#f5f5f5',
			}}
		>
			<Container maxWidth="xs">
				<Paper elevation={3} sx={{ padding: 4, borderRadius: 2 }}>
					<Typography variant="h5" align="center" gutterBottom>
						Login
					</Typography>

					{error && (
						<Alert severity="error" sx={{ mb: 2 }}>
							{error}
						</Alert>
					)}

					<Box component="form" noValidate autoComplete="off" onSubmit={handleSubmit}>
						<TextField
							label="Nome"
							variant="outlined"
							fullWidth
							margin="normal"
							required
							onChange={(e) => setName(e.target.value)}
						/>
						<TextField
							label="Usuario"
							variant="outlined"
							fullWidth
							margin="normal"
							required
							onChange={(e) => setUsername(e.target.value)}
						/>
						<TextField
							label="Senha"
							type="password"
							variant="outlined"
							fullWidth
							margin="normal"
							required
							onChange={(e) => setPassword(e.target.value)}
						/>
						<Button
							type="submit"
							variant="contained"
							color="primary"
							fullWidth
							sx={{ mt: 2 }}
							disabled={isLoading}
						>
							{isLoading ? (
								<CircularProgress size={24} color="inherit" />
							) : (
								'Entrar'
							)}
						</Button>

						<Box textAlign="center" mt={2}>
							<Typography variant="body2">
								Não tem uma conta?{' '}
								<MuiLink component={Link} to="/registro">
									Cadastre-se
								</MuiLink>
							</Typography>
						</Box>
					</Box>
				</Paper>
			</Container>
		</Box>
    )
}

export default Register;
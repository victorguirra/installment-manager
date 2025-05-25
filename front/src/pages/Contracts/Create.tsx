import { useState } from "react";
import { useNavigate } from "react-router-dom";

import { createContract } from "@/services/contractService";

import {
    Box,
    Button,
    Container,
    TextField,
    Typography,
    Paper,
} from '@mui/material';

const CreateContract = () => {
    const [description, setDescription] = useState<string>('');
    const [totalAmount, setTotalAmount] = useState<number>(0);
    const [installmentAmount, setInstallmentAmount] = useState<number>(0);

    const navigate = useNavigate();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        await createContract(description, totalAmount, installmentAmount);
        navigate('/contracts');
    };

    return (
        <Box
            sx={{
                minHeight: '100vh',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                backgroundColor: '#f5f5f5',
            }}
        >
            <Container maxWidth="sm">
                <Paper elevation={3} sx={{ p: 4, borderRadius: 2 }}>
                    <Typography variant="h5" align="center" gutterBottom>
                        Novo Contrato
                    </Typography>

                    <Box component="form" onSubmit={handleSubmit}>
                        <TextField
                            label="Descrição"
                            variant="outlined"
                            fullWidth
                            required
                            margin="normal"
                            defaultValue={description}
                            onChange={(e) => setDescription(e.target.value)}
                        />

                        <TextField
                            label="Valor Total"
                            variant="outlined"
                            fullWidth
                            required
                            margin="normal"
                            type="number"
                            inputProps={{ step: '0.01' }}
                            defaultValue={totalAmount}
                            onChange={(e) => setTotalAmount(Number(e.target.value))}
                        />

                        <TextField
                            label="Número de Parcelas"
                            variant="outlined"
                            fullWidth
                            required
                            margin="normal"
                            type="number"
                            inputProps={{ step: '1', min: '1' }}
                            defaultValue={installmentAmount}
                            onChange={(e) => setInstallmentAmount(Number(e.target.value))}
                        />

                        <Button
                            type="submit"
                            variant="contained"
                            color="primary"
                            fullWidth
                            sx={{ mt: 2 }}
                        >
                            Criar Contrato
                        </Button>
                    </Box>
                </Paper>
            </Container>
        </Box>
    )
}

export default CreateContract;
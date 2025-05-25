import { useEffect, useState } from "react";
import { Link } from "react-router-dom";

import { deleteContract, getContracts } from "@/services/contractService";
import type { IContract } from "@/types/ContractTypes";

import {
    Box,
    Button,
    Container,
    Typography,
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableRow,
    Paper,
    Dialog,
    DialogTitle,
    DialogContent,
    DialogContentText,
    DialogActions,
    CircularProgress
} from '@mui/material';

const Contracts = () => {
    const [contracts, setContracts] = useState<IContract[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(false);

    const [selectedContract, setSelectedContract] = useState<IContract | null>(null);
    const [confirmDialogOpen, setConfirmDialogOpen] = useState(false);

    const handleGetContracts = async () => {
        try {
            setIsLoading(true);

            const contractsResponse: IContract[] = await getContracts();
            setContracts(contractsResponse);
        }
        catch { }
        finally {
            setIsLoading(false);
        }
    }

    const handleOpenDeleteDialog = (contract: IContract) => {
        setSelectedContract(contract);
        setConfirmDialogOpen(true);
    };

    const handleCloseDialog = () => {
        setConfirmDialogOpen(false);
        setSelectedContract(null);
    };

    const handleConfirmDelete = async () => {
        if (selectedContract) {
            setIsLoading(true);
            
            await deleteContract(selectedContract.id);
            handleGetContracts();
            handleCloseDialog();

            setIsLoading(false);
        }
    };

    useEffect(() => {
        handleGetContracts();
    }, [])

    return (
        <Container sx={{ mt: 4 }}>
            <Box
                sx={{
                    display: 'flex',
                    justifyContent: 'space-between',
                    alignItems: 'center',
                    mb: 3,
                }}
            >
                <Typography variant="h5">Meus Contratos</Typography>
                <Button
                    variant="contained"
                    component={Link}
                    to="/contracts/create"
                >
                    + Novo Contrato
                </Button>
            </Box>

            {isLoading ? (
                <Box
                    sx={{
                        display: 'flex',
                        justifyContent: 'center',
                        alignItems: 'center',
                        minHeight: '200px',
                    }}
                >
                    <CircularProgress />
                </Box>
            ) : (
                <Paper elevation={2}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Descrição</TableCell>
                                <TableCell>Qtd. Parcelas</TableCell>
                                <TableCell>Data de Criação</TableCell>
                                <TableCell>Ações</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {contracts.map((contract) => (
                                <TableRow key={contract.id}>
                                    <TableCell>{contract.description}</TableCell>
                                    <TableCell>{contract.installments.length}</TableCell>
                                    <TableCell>{new Date(contract.createdAt).toLocaleDateString()}</TableCell>
                                    <TableCell>
                                        <Box sx={{ display: 'flex', gap: 1 }}>
                                            <Button
                                                variant="contained"
                                                component={Link}
                                                to={`/contracts/${contract.id}/installments`}
                                            >
                                                Ver parcelas
                                            </Button>
                                            <Button
                                                variant="outlined"
                                                size="small"
                                                color="error"
                                                onClick={() => handleOpenDeleteDialog(contract)}
                                            >
                                                Excluir
                                            </Button>
                                        </Box>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </Paper>
            )}

            <Dialog open={confirmDialogOpen} onClose={handleCloseDialog}>
                <DialogTitle>Confirmar Exclusão</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        Deseja excluir o contrato "<strong>{selectedContract?.description}</strong>"?
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleCloseDialog}>Cancelar</Button>
                    <Button onClick={handleConfirmDelete} color="error" variant="contained">
                        Excluir
                    </Button>
                </DialogActions>
            </Dialog>
        </Container>
    );
}

export default Contracts;
import { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";
import axios from 'axios';

import { 
    anticipateInstallments,
    approveAnticipation,
    getAnticipations,
    getInstallments,
    rejectAnticipation
} from "@/services/installmentService";

import type { IInstallment, IInstallmentAnticipation } from "@/types/InstallmentTypes";

import {
    Box,
    Button,
    Checkbox,
    CircularProgress,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Typography,
    Paper,
} from '@mui/material';
import type { InstallmentStatusEnum } from "@/enums/InstallmentStatus";


const Installments = () => {
    const { id } = useParams<{ id: string }>();
    const contractId = parseInt(id as string);

    const [installments, setInstallments] = useState<IInstallment[]>([]);
    const [anticipations, setAnticipations] = useState<IInstallmentAnticipation[]>([]);

    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [selectMode, setSelectMode] = useState<boolean>(false);
    const [selectedIds, setSelectedIds] = useState<number[]>([]);
    const [message, setMessage] = useState<{ text: string; type: 'error' | 'success' | 'warning' | null }>({ text: '', type: null });


    const handleGetInstallmentData = async () => {
        try {
            setIsLoading(true);

            const installmentsResponse: IInstallment[] = await getInstallments(contractId);
            setInstallments(installmentsResponse);

            const anticipationsResponse: IInstallmentAnticipation[] = await getAnticipations();
            setAnticipations(anticipationsResponse);
        }
        catch { }
        finally {
            setIsLoading(false);
        }
    }

    useEffect(() => {
        handleGetInstallmentData();
    }, [])

    const handleToggleSelectMode = () => {
        setSelectMode(!selectMode);
        setSelectedIds([]);
        setMessage({ text: '', type: null });
    };

    const handleSelect = (id: number) => {
        setSelectedIds((prev) =>
            prev.includes(id) ? prev.filter((x) => x !== id) : [...prev, id]
        );
    };

    const handleRequestAnticipation = async () => {
        if (selectedIds.length === 0) {
            setMessage({ text: 'Selecione ao menos uma parcela.', type: 'warning' });
            return;
        }

        try {
            await anticipateInstallments(selectedIds);
            setMessage({ text: 'Solicitação enviada com sucesso.', type: 'success' });
            
            await handleGetInstallmentData();
            setSelectMode(false);
        }
        catch (error: any) {
            if(axios.isAxiosError(error)){
                setMessage({ text: error.response?.data || 'Erro ao solicitar.', type: 'error' });
            }
            else{
                setMessage({ text: error.message || 'Erro ao solicitar.', type: 'error' });
            }
        }
    };

    const handleApprove = async (anticipationId: number) => {
        try {
            await approveAnticipation(anticipationId);
            setMessage({ text: 'Solicitação aprovada.', type: 'success' });
            
            await handleGetInstallmentData();
        }
        catch (error: any) {
            if(axios.isAxiosError(error)){
                setMessage({ text: error.response?.data || 'Erro ao solicitar.', type: 'error' });
            }
            else{
                setMessage({ text: error.message || 'Erro ao aprovar.', type: 'error' });
            }
        }
    };

    const handleReject = async (anticipationId: number) => {
        try {
            await rejectAnticipation(anticipationId);
            setMessage({ text: 'Solicitação rejeitada.', type: 'success' });
            await handleGetInstallmentData();
        }
        catch (error: any) {
            if(axios.isAxiosError(error)){
                setMessage({ text: error.response?.data || 'Erro ao solicitar.', type: 'error' });
            }
            else{
                setMessage({ text: error.message || 'Erro ao rejeitar.', type: 'error' });
            }
        }
    };

    const getInstallmentStatus = (status: InstallmentStatusEnum): string => {
        switch (status) {
            case 1:
                return 'Em Aberto';
            case 2:
                return 'Pago';
            default:
                return 'Desconhecido';
        }
    }

    const getAnticipationStatus = (installmentId: number): string => {
        const anticipation = anticipations.find((a) => a.installmentId === installmentId);
        
        if (!anticipation)
            return 'Não antecipada';
        
        switch (anticipation.status) {
            case 1:
                return 'Pendente';
            case 2:
                return 'Aprovada';
            case 3:
                return 'Rejeitada';
            default:
                return 'Desconhecido';
        }
    };

    return (
        <Box p={4}>
            <Typography variant="h4" gutterBottom>
                Parcelas do Contrato
            </Typography>

            {message.text && (
                <Typography color={message.type === 'error' ? 'error' : message.type === 'success' ? 'green' : 'orange'} sx={{ mb: 2 }}>
                    {message.text}
                </Typography>
            )}

            <Box mb={2} display="flex" gap={2}>
                <Button variant="outlined" onClick={handleToggleSelectMode}>
                    {selectMode ? 'Cancelar' : 'Antecipar Parcelas'}
                </Button>
                {selectMode && (
                    <Button variant="contained" onClick={handleRequestAnticipation}>
                        Enviar Solicitação
                    </Button>
                )}
            </Box>

            {isLoading ? (
                <CircularProgress />
            ) : (
                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                {selectMode && <TableCell />}
                                <TableCell>Código</TableCell>
                                <TableCell>Vencimento</TableCell>
                                <TableCell>Valor</TableCell>
                                <TableCell>Status Parcela</TableCell>
                                <TableCell>Status Antecipação</TableCell>
                                <TableCell>Ações</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {installments.sort((a, b) => a.id - b.id).map((installment) => {
                                const anticipation = anticipations.find((a) => a.installmentId === installment.id);
                                return (
                                    <TableRow key={installment.id}>
                                        {selectMode && (
                                            <TableCell>
                                                <Checkbox
                                                    checked={selectedIds.includes(installment.id)}
                                                    onChange={() => handleSelect(installment.id)}
                                                    disabled={!!anticipation}
                                                />
                                            </TableCell>
                                        )}
                                        <TableCell>{installment.code}</TableCell>
                                        <TableCell>{new Date(installment.dueDate).toLocaleDateString()}</TableCell>
                                        <TableCell>R$ {installment.amount.toFixed(2)}</TableCell>
                                        <TableCell>{getInstallmentStatus(installment.status)}</TableCell>
                                        <TableCell>{getAnticipationStatus(installment.id)}</TableCell>
                                        <TableCell>
                                            {anticipation && anticipation.status === 1 && (
                                                <>
                                                    <Button size="small" color="success" onClick={() => handleApprove(anticipation.id)}>
                                                        Aprovar
                                                    </Button>
                                                    <Button size="small" color="error" onClick={() => handleReject(anticipation.id)}>
                                                        Reprovar
                                                    </Button>
                                                </>
                                            )}
                                        </TableCell>
                                    </TableRow>
                                );
                            })}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}

            <Box mt={2}>
                <Button
                    variant="contained"
                    component={Link}
                    to="/contracts"
                    
                >
                    Voltar para contratos
                </Button>
            </Box>
        </Box>
    );
}

export default Installments;
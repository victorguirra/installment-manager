import { Routes, Route, Navigate } from "react-router-dom";
import { PrivateRoute } from "./privateRoute";

import Login from '@/pages/Login';
import Register from '@/pages/Register';

import Contracts from '@/pages/Contracts';
import CreateContract from '@/pages/Contracts/Create';

import Installments from "@/pages/Installments";

export default function AppRoutes(){
    return(
        <Routes>
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />

            <Route element={<PrivateRoute />}>
                <Route path="/" element={<Navigate to="/contracts" replace />} />
                <Route path="/contracts" element={<Contracts />} />
                <Route path="/contracts/create" element={<CreateContract />} />
                
                <Route path="/contracts/:id/installments" element={<Installments />} />
            </Route>
        </Routes>
    )
}